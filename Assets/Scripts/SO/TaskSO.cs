using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskState {
    Waiting,
    Excuting,
    Completed,
    End
}
[CreateAssetMenu]
public class TaskSO : ScriptableObject
{
    public TaskState taskState;
    public string[] taskInStartDialogue;
    public string[] taskInExcutingDialogue;
    public string[] taskInCompletedDialogue;
    public string[] taskInEndDialogue;
    public ItemSO startReward;
    public ItemSO endReward;

    public int enemyCountNeed = 3;
    public int enemyCountCurrent = 0;

    public void Start() //  任务开始
    {
        EventCenter.onEnemyDied += TaskUpdateOnEnemyDied;
        enemyCountCurrent = 0;
        taskState = TaskState.Excuting;
    }
    public void Deliver() // 任务交付
    {
        EventCenter.onEnemyDied -= TaskUpdateOnEnemyDied;
        taskState = TaskState.End;
    }

    public void TaskUpdateOnEnemyDied(Enemy enemy)
    {
        enemyCountCurrent++;
        if (enemyCountCurrent >= enemyCountNeed)
        {
            taskState = TaskState.Completed;
        }
    }
}
