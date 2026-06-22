using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{

    private NavMeshAgent playerAgent;
    private Animator playerAnimator;
    private bool haveInteracted = false;
    public void OnClick(NavMeshAgent playerAgent)
    { 
        this.playerAgent = playerAgent;
        playerAgent.stoppingDistance = 2.5f;
        playerAgent.SetDestination(transform.position);  
        haveInteracted = false;
        if(playerAgent != null)
        {
            playerAnimator = playerAgent.GetComponent<Animator>();
            if(playerAnimator != null)
            {
                    playerAnimator.SetBool("isWalking", true);
            }
        }
    }
    private void Update()
    {
        if(playerAgent != null && !haveInteracted && playerAgent.pathPending == false )
        {
            if(playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                Interact();
                if(playerAnimator != null)
                {
                    playerAnimator.SetBool("isWalking", false);
                }
                haveInteracted = true;
            }
        }
    }
    protected virtual void Interact()
    {
        Debug.Log("Interacted with interactable obj");
    }
}
