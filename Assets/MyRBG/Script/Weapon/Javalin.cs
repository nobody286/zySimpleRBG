using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javalin : Weapon
{
    public float bulletSpeed = 5;
    public GameObject bulletPrefab;
    private GameObject bulletGo;
    private void Start()
    {
       SpawnBullet();
    }
    public override void Attack()
    {
        if(bulletGo != null && bulletGo.transform.parent != null)
        {
            bulletGo.transform.parent = null;
        }

        if (bulletGo != null)
        {
            bulletGo.GetComponent<Rigidbody>().velocity = bulletGo.transform.up * bulletSpeed;
            bulletGo = null;
            Invoke("SpawnBullet", 0.5f);
        }
        else
        {
            return;
        }
    }
    private void SpawnBullet()
    {
        bulletGo = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        bulletGo.transform.parent = transform;
    }
}


