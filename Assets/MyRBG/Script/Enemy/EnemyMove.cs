using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
    public int HP = 100;

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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(100);
        }

    }
    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, -1f));
        return transform.position + randomDir.normalized * Random.Range(3, 5);
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            GetComponent<Collider>().enabled = false;
            int count = 4;   //掉落物个数
            for(int i = 0; i < count; i++)
            {
                ItemScriptObject item =  ItemDBManager.Instance.GetRandomItem();
                GameObject.Instantiate(item.prefab , transform.position , Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    void OnFootstep()
    {

    }
}
