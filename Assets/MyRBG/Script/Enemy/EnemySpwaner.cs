using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spwanTime;
    private float spwanTimer;
    // Start is called before the first frame update
    void Start()
    {
        SpwanEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        spwanTimer += Time.deltaTime;
        if( spwanTimer > spwanTime)
        {
            spwanTimer = 0;
            SpwanEnemy();
        }
    }
    void SpwanEnemy()
    {
        GameObject.Instantiate(enemyPrefab , transform.position , Quaternion.identity);
    }
}
