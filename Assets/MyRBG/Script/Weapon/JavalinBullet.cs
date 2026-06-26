using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class JavalinBullet : MonoBehaviour
{
    private Rigidbody rgd;
    private Collider col;
    private void Start()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == TagManager.PLAYER || this.tag == TagManager.INTERACTABLE)
        {
            Debug.Log("collider.tag is player or this.tag is interactable");
            return;
        }
        rgd.velocity = Vector3.zero;
        rgd.isKinematic = true;
        col.enabled = false;
        Destroy(this.gameObject, 2f);
    }
}
