using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
    public int HP = 100;
    public int exp = 20;
    Animator animator;
    public enum EnemyState
    {
        NormalState,
        FightState,
        MovingState,
        RestingState
    }

    private EnemyState state = EnemyState.NormalState;    //初始状态
    private EnemyState childState = EnemyState.RestingState;
    private NavMeshAgent enemyAgent;

    public float restTime = 2;
    private float restTimer = 0;

    [Header("转向设置")]
    [Tooltip("移动时 NavMeshAgent 的角速度（度/秒），增大可让转身更快）")]
    public float moveAngularSpeed = 360f;
    [Tooltip("静止或默认的角速度（会在 Start 时自动读取并恢复）")]
    public float defaultAngularSpeed = 120f;

    [Header("掉落设置")]
    [Tooltip("掉落物相对于敌人位置的最大散布半径（XZ 平面）")]
    public float dropRadius = 0.8f;
    [Tooltip("生成点相对于地面的竖直偏移，防止穿地")]
    public float spawnHeightOffset = 0.5f;

    [Header("玩家检测设置")]
    [Tooltip("检测玩家的半径，当玩家进入该半径时开始追踪")]
    public float detectRadius = 5f;
    [Tooltip("玩家超出该半径时放弃追踪（带滞回，避免频繁切换）")]
    public float loseTargetRadius = 7f;

    [Header("追击停下设置")]
    [Tooltip("靠近玩家到该距离时停下（例如近战范围）")]
    public float stopDistance = 1.5f;
    [Tooltip("从停下状态恢复追击需要超过此距离（必须 > stopDistance）")]
    public float resumeChaseDistance = 2.5f;

    private Transform playerTransform;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 读取当前 agent 的角速度并作为默认值（如果在 Inspector 未覆盖）
        if (enemyAgent != null)
        {
            defaultAngularSpeed = enemyAgent.angularSpeed;
        }

        var playerGO = GameObject.FindGameObjectWithTag(TagManager.PLAYER);
        if (playerGO != null)
            playerTransform = playerGO.transform;
    }
    void Update()
    {
        // 先检测玩家是否在探测范围内（优先于巡逻逻辑）
        if (playerTransform != null)
        {
            float distToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (state != EnemyState.FightState && distToPlayer <= detectRadius)
            {
                // 进入追击状态
                state = EnemyState.FightState;
                // 进入追击时让 agent 开始移动并加快转向
                if (enemyAgent != null)
                {
                    enemyAgent.angularSpeed = moveAngularSpeed;
                    enemyAgent.isStopped = false;
                    enemyAgent.SetDestination(playerTransform.position);
                }
                animator.SetBool("isWalking", true);
            }
            else if (state == EnemyState.FightState)
            {
                // 仍在追击中
                if (distToPlayer > loseTargetRadius)
                {
                    // 失去目标，回到巡逻（恢复为 NormalState）
                    state = EnemyState.NormalState;
                    childState = EnemyState.RestingState;
                    restTimer = 0;

                    if (enemyAgent != null)
                    {
                        enemyAgent.angularSpeed = defaultAngularSpeed;
                        enemyAgent.isStopped = false;
                    }
                    animator.SetBool("isWalking", false);
                }
                else
                {
                    // 当在追击状态内，处理靠近停下与恢复追击的界定范围（带滞回）
                    if (distToPlayer <= stopDistance)
                    {
                        // 到达停下距离，停止移动但保持朝向（NavMeshAgent 停止）
                        if (enemyAgent != null)
                        {
                            enemyAgent.isStopped = true;
                        }
                        animator.SetBool("isWalking", false);
                    }
                    else if (distToPlayer >= resumeChaseDistance)
                    {
                        // 超出恢复距离，继续追击
                        if (enemyAgent != null)
                        {
                            enemyAgent.isStopped = false;
                            if (!enemyAgent.pathPending)
                                enemyAgent.SetDestination(playerTransform.position);
                        }
                        animator.SetBool("isWalking", true);
                    }
                    else
                    {
                        // 在 stopDistance 与 resumeChaseDistance 之间，保持当前状态（避免抖动）
                        if (enemyAgent != null && !enemyAgent.isStopped)
                        {
                            // 仍保持追踪目标位置
                            if (!enemyAgent.pathPending)
                                enemyAgent.SetDestination(playerTransform.position);
                            animator.SetBool("isWalking", true);
                        }
                        else
                        {
                            animator.SetBool("isWalking", false);
                        }
                    }
                }
                // 跳过默认巡逻逻辑当处于追击状态
                return;
            }
        }

        // 巡逻逻辑（仅在 NormalState 下执行）
        if (state == EnemyState.NormalState)
        {
            if (childState == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;

                if (restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    if (enemyAgent != null)
                    {
                        enemyAgent.SetDestination(randomPosition);

                        // 切换到移动状态时提高角速度，让转身更快
                        enemyAgent.angularSpeed = moveAngularSpeed;
                    }

                    childState = EnemyState.MovingState;
                    animator.SetBool("isWalking", true);
                }
            }
            else if (childState == EnemyState.MovingState)
            {
                // 使用 remainingDistance 与 stoppingDistance 判断是否到达目的地
                if (enemyAgent != null && !enemyAgent.pathPending && enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    // 到达后恢复原先角速度
                    enemyAgent.angularSpeed = defaultAngularSpeed;

                    restTimer = 0;
                    animator.SetBool("isWalking", false);
                    childState = EnemyState.RestingState;
                }
            }
        }

        //测试代码
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(100);
        }
    }
    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, 1f));
        return transform.position + randomDir.normalized * Random.Range(2, 4);
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 禁用敌人的碰撞体，避免掉落和敌人发生干扰
        var col = GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }
        int dropCount = Random.Range(1, 4);
        // 掉落若干物品，位置略微分散
        SpwanPickableItem(dropCount);
        EventCenter.EnemyDied(this);
        Destroy(this.gameObject);
    }
    public void SpwanPickableItem(int dropcount)
    {
        for (int i = 0; i < dropcount; i++)
        {
            ItemScriptObject item = ItemDBManager.Instance.GetRandomItem();
            if (item == null || item.prefab == null)
            {
                continue;
            }

            // 在 XZ 平面上随机散布，附加一个小的高度偏移以防穿地
            Vector2 offset = Random.insideUnitCircle * dropRadius;
            Vector3 spawnPos = transform.position + new Vector3(offset.x, spawnHeightOffset, offset.y);

            // 随机旋转，使掉落物朝向不一致
            GameObject go = GameObject.Instantiate(item.prefab, spawnPos, Random.rotation);
            go.tag = TagManager.INTERACTABLE;
            pickableObject po = go.AddComponent<pickableObject>();
            po.itemSO = item;

            Collider collider = go.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
                collider.isTrigger = false;
            }
            Rigidbody rgd = go.GetComponent<Rigidbody>();
            if (rgd != null)
            {
                rgd.isKinematic = false;
                rgd.useGravity = true;
            }
        }
    }

    void OnFootstep()
    {

    }
}