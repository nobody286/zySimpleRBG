using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    public static event Action<EnemyMove> OnEnemyDied;
    public static void EnemyDied(EnemyMove enemy)
    {
        OnEnemyDied?.Invoke(enemy);
    }
}
