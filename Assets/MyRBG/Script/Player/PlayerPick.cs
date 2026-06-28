using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == TagManager.INTERACTABLE)
        {
            Debug.Log("the tag is right");
            pickableObject po = collision.gameObject.GetComponent<pickableObject>();
            if(po != null)
            {

                    InventoryManager.Instance.Additem(po.itemSO);
                    Destroy(po.gameObject);
            }
        }
    }
}
