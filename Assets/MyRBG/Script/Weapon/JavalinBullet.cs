using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class JavalinBullet : MonoBehaviour
{
    public int atkValue = 30;
    private Rigidbody rgd;
    private Collider col;
    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == TagManager.PLAYER || this.tag == TagManager.INTERACTABLE)
        {
            return;
        }

        if (rgd != null)
        {
            rgd.velocity = Vector3.zero;
            rgd.isKinematic = true;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        transform.parent = collision.gameObject.transform;
        Destroy(this.gameObject, 2f);

        if (collision.gameObject.tag == TagManager.ENEMY)
        {
            var em = collision.gameObject.GetComponent<EnemyMove>();
            if (em != null)
            {
                em.TakeDamage(atkValue);
            }
        }
    }
}
