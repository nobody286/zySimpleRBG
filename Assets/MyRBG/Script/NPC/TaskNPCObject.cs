using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPCObject : InteractableObject
{
    public string npcName;
    public GameTaskSO gameTaskSO;
    protected override void Interact()
    {
        DialogUI.Instance.Show(npcName , gameTaskSO.diague , OnDialogueEnd);
    }
    private void Start()
    {
        gameTaskSO.state = GameTaskState.Waiting;
    }
    public void OnDialogueEnd()
    {
        print("get");
        switch (gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                gameTaskSO.state = GameTaskState.Executing;
                InventoryManager.Instance.Additem(gameTaskSO.startReward);
                break;
            case GameTaskState.Executing:
                break;
            case GameTaskState.Completed:
                break;
            case GameTaskState.End:
                break;
            default:
                break;
        }
    }
   
}
