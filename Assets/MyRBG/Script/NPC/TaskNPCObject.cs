using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPCObject : InteractableObject
{
    public string npcName;
    public GameTaskSO gameTaskSO;
    public string[] contentInTaskExcuting;
    public string[] contentInTaskCompleted;
    public string[] contentInTaskEnd;

    protected override void Interact()
    {
        switch(gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                // 不在这里直接 Start()，先显示对话，接受后在 OnDialogueEnd 里开始任务并发放 startReward
                DialogUI.Instance.Show(npcName, gameTaskSO.diague, OnDialogueEnd);
                break;
            case GameTaskState.Executing:
                DialogUI.Instance.Show(npcName, contentInTaskExcuting);
                break;
            case GameTaskState.Completed:
                DialogUI.Instance.Show(npcName, contentInTaskCompleted, OnDialogueEnd);
                break;
            case GameTaskState.End:
                DialogUI.Instance.Show(npcName, contentInTaskEnd);
                break;
            default:
                break;
        }
      
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
                // 在玩家确认对话后再真正开始任务并发放奖励
                gameTaskSO.Start(); // 会把 state 设为 Executing 并订阅事件
                InventoryManager.Instance.Additem(gameTaskSO.startReward);
                MessageUI.Instance.Show("你接受了一个任务!");
                break;
            case GameTaskState.Executing:
                break;
            case GameTaskState.Completed:
                InventoryManager.Instance.Additem(gameTaskSO.endReward);
                gameTaskSO.End();
                MessageUI.Instance.Show("你完成了一个任务!");
                break;
            case GameTaskState.End:
                break;
            default:
                break;
        }
    }
   
}
