using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent enemyAgent;
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        
    }
}
