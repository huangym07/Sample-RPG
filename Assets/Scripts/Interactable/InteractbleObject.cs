using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractbleObject : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private bool haveInteracted = false;
    private const int interactDistance = 2;
    public void OnClick(NavMeshAgent playerAgent) 
    {
        playerAgent.stoppingDistance = interactDistance;
        playerAgent.SetDestination(transform.position);

        this.playerAgent = playerAgent;
        haveInteracted = false;
        // Interact();
    }

    private void Update()
    {
        if (playerAgent != null && haveInteracted == false && playerAgent.pathPending == false)
        {
            // playerAgent 不为空，并且路径计算完了（如果没计算完，那么 remainingDistance 可能为 0）
            if (playerAgent.remainingDistance <= interactDistance)
            {
                Interact();
                haveInteracted = true;
            }
        }
    }
    protected virtual void Interact()
    {
        print("Interacting with Interacrable Object");
    }
}
