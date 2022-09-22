using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentBehavior : MonoBehaviour
{
    enum TaskEnum
    {
        PlayCard,
        Attack
    }
    TaskList<TaskEnum> _taskList = new TaskList<TaskEnum>();
    public void StartTask()
    {

    }

    public void SelectTasks()
    {
        _taskList.DefineTask(TaskEnum.PlayCard,OnPlayCardEnter, DoNothingUpdate, DoNothing);
        _taskList.DefineTask(TaskEnum.Attack, OnAttackEnter, DoNothingUpdate, DoNothing);
    }

    void OnPlayCardEnter()
    {

    }

    void OnAttackEnter()
    {

    }

    void DoNothing()
    {

    }

    bool DoNothingUpdate()
    {
        return true;
    }
}
