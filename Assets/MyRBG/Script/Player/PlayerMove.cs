using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private Animator animator;

 
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (playerAgent == null || animator == null) return;
        //========================移动============================
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            Ray ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider =  Physics.Raycast(ray, out  hit);
            if (isCollider)
            {
                if(hit.collider.tag == TagManager.GROUND)
                {
                    playerAgent.stoppingDistance = 0f;
                    playerAgent.SetDestination(hit.point);
                    if(animator != null)
                    {
                        animator.SetBool("isWalking", true);
                    }
                }
                else if(hit.collider.tag == "Interactable")
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);

                }
            }
        }
        if(playerAgent != null && playerAgent.pathPending == false)
        {
            if(playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                if(animator != null)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }

    }
}
