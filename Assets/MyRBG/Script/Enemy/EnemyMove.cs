using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyMove : MonoBehaviour
{
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
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(state == EnemyState.NormalState)
        {
            if(childState == EnemyState.RestingState)
            {
                restTimer += Time.deltaTime;

                if(restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MovingState;
                }
            }
            else if(childState == EnemyState.MovingState)
            {
                if(enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;
                    childState = EnemyState.RestingState;
                }
            }
        }
    }
    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, -1f));
        return transform.position + randomDir.normalized * Random.Range(3, 8);
    }
}
