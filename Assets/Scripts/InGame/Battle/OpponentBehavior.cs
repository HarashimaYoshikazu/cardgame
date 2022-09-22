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

    /// <summary>
    /// シーン読み込み時に1度だけ呼ばれる
    /// </summary>
    public void InitTask()
    {
        _taskList.DefineTask(TaskEnum.PlayCard, OnPlayCardEnter, DoNothingUpdate, DoNothing);
        _taskList.DefineTask(TaskEnum.Attack, OnAttackEnter, DoNothingUpdate, DoNothing);
    }

    /// <summary>
    /// ターンの最初に呼ばれる
    /// </summary>
    public void SelectTasks()
    {
        _taskList.AddTask(TaskEnum.PlayCard);
        _taskList.AddTask(TaskEnum.Attack);
    }

    public bool IsEnd
    {
        get { return _taskList.IsEnd; }
    }

    void OnPlayCardEnter()
    {
        //BattleManager.Instance.Enemy.Hands
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
