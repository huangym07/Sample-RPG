using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskNPCObejct : InteractbleObject
{
    public string taskNPCName;
    public TaskSO task;

    public void Start()
    {
        task.taskState = TaskState.Waiting;
    }
    protected override void Interact()
    {
        switch(task.taskState)
        {
            case TaskState.Waiting:
                DialogueUI.Instance.Show(taskNPCName, task.taskInStartDialogue, OnDialogueFinish); // 加入一个回调函数；将 OnDialogFinish 传过去，Show() 中用 Action 类型的委托来接收，并保存起来；当对话结束时，通过保存起来的 Action 类型的委托的引用，引用了这个 OnDialogFinish
                break;
            case TaskState.Excuting:
                DialogueUI.Instance.Show(taskNPCName, task.taskInExcutingDialogue);
                break;
            case TaskState.Completed: 
                DialogueUI.Instance.Show(taskNPCName, task.taskInCompletedDialogue, OnDialogueFinish);
                break;
            case TaskState.End:
                DialogueUI.Instance.Show(taskNPCName, task.taskInEndDialogue);
                break;
        }
    }
    public void OnDialogueFinish()
    {
        switch(task.taskState)
        {
            case TaskState.Waiting:
                task.Start();
                InventoryManager.Instance.AddItem(task.startReward);
                break;
            case TaskState.Excuting: break;
            case TaskState.Completed:
                task.Deliver();
                InventoryManager.Instance.AddItem(task.endReward);
                break;    
            case TaskState.End: break;
        }
    }
}
