using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // 读取当前 agent 的角速度并作为默认值（如果在 Inspector 未覆盖）
        if (enemyAgent != null)
        {
            defaultAngularSpeed = enemyAgent.angularSpeed;
        }
    }
    void Update()
    {
        if (state == EnemyState.NormalState)
        {
            if (childState == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;

                if (restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);

                    // 切换到移动状态时提高角速度，让转身更快
                    if (enemyAgent != null)
                    {
                        enemyAgent.angularSpeed = moveAngularSpeed;
                    }

                    childState = EnemyState.MovingState;
                    animator.SetBool("isWalking", true);
                }
            }
            else if (childState == EnemyState.MovingState)
            {
                // 使用 remainingDistance 与 stoppingDistance 判断是否到达目的地
                if (!enemyAgent.pathPending && enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
                {
                    // 到达后恢复原先角速度
                    if (enemyAgent != null)
                    {
                        enemyAgent.angularSpeed = defaultAngularSpeed;
                    }

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
            if(collider != null)
            {
                collider.enabled = true;
                collider.isTrigger = false;
            }
            Rigidbody rgd = go.GetComponent<Rigidbody>();
            if(rgd != null)
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