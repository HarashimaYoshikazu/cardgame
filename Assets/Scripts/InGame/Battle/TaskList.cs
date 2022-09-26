using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class TaskList<T> where T : Enum
{
	private class Task
	{
		public T TaskType;
		public Action Enter { get; set; }
		public Func<bool> Update { get; set; }
		public Action Exit { get; set; }

		public Task(T t, Action enter, Func<bool> update, Action exit)
		{
			TaskType = t;
			Enter = enter;
			Update = update ?? delegate { return true; };
			Exit = exit;
		}
	}

	/// <summary> 定義されたタスク </summary>
	Dictionary<T, Task> _defineTaskDictionary = new Dictionary<T, Task>();
	/// <summary> 現在積まれているタスク </summary>
	List<Task> _currentTaskList = new List<Task>();
	/// <summary> 現在動作しているタスク </summary>
	Task _currentTask = null;
	/// <summary> 現在のIndex番号 </summary>
	int _currentIndex = 0;

	/// <summary>
	/// 追加されたタスクがすべて終了しているか
	/// </summary>
	public bool IsEnd
	{
		get { return _currentTaskList.Count <= _currentIndex; }
	}

	/// <summary>
	///  タスクが動いているか
	/// </summary>
	public bool IsMoveTask
	{
		get { return _currentTask != null; }
	}

	/// <summary>
	/// 現在のタスクタイプ
	/// </summary>
	public T CurrentTaskType
	{
		get
		{
			if (_currentTask == null)
				return default(T);
			return _currentTask.TaskType;
		}
	}

	/// <summary>
	/// 追加されているタスクのリスト
	/// </summary>
	public List<T> CurrentTaskTypeList
	{
		get
		{
			return _currentTaskList.Select(x => x.TaskType).ToList();
		}
	}

	/// <summary>
	/// 現在のインデックス
	/// </summary>
	public int CurrentIndex
	{
		get { return _currentIndex; }
	}

	/// <summary>
	/// 毎フレーム呼ばれる処理
	/// (BehaviourのUpdateで呼ばれる想定)
	/// </summary>
	public void UpdateTask()
	{
		// タスクがなければ何もしない
		if (IsEnd)
		{
			return;
		}

		// 現在のタスクがなければ、タスクを取得する
		if (_currentTask == null)
		{
			_currentTask = _currentTaskList[_currentIndex];
			// Enterを呼ぶ
			_currentTask.Enter?.Invoke();
		}

		// タスクのUpdateを呼ぶ
		var isEndOneTask = _currentTask.Update();

		// タスクが終了していれば次の処理を呼ぶ
		if (isEndOneTask)
		{
			// 現在のタスクのExitを呼ぶ
			_currentTask?.Exit();

			// Index追加
			_currentIndex++;

			// タスクがなければクリアする
			if (IsEnd)
			{
				_currentIndex = 0;
				_currentTask = null;
				_currentTaskList.Clear();
				return;
			}

			// 次のタスクを取得する
			_currentTask = _currentTaskList[_currentIndex];
			// 次のタスクのEnterを呼ぶ
			_currentTask?.Enter();
		}
	}

	/// <summary>
	/// タスクの定義
	/// </summary>
	/// <param name="t"></param>
	/// <param name="enter"></param>
	/// <param name="update"></param>
	/// <param name="exit"></param>
	public void DefineTask(T t, Action enter, Func<bool> update, Action exit)
	{
		var task = new Task(t, enter, update, exit);
		var exist = _defineTaskDictionary.ContainsKey(t);
		if (exist)
		{
#if UNITY_EDITOR
			Debug.LogError($"{GetType()}は既に追加されています。(登録されませんでした).");
#endif
			return;
		}
		_defineTaskDictionary.Add(t, task);
	}

	public void DefineTask(T t, Action enter,Action exit)
	{
		DefineTask(t,enter,()=>true,exit);
	}

	/// <summary>
	/// タスクの登録
	/// </summary>
	/// <param name="t"></param>
	public void AddTask(T t)
	{
		Task task = null;
		var exist = _defineTaskDictionary.TryGetValue(t, out task);
		if (exist == false)
		{
#if UNITY_EDITOR
			Debug.LogError($"{GetType()}のタスクが登録されていないので追加できません.");
#endif
			return;
		}
		_currentTaskList.Add(task);
	}

	/// <summary>
	/// 強制終了
	/// </summary>
	public void ForceStop()
	{
		if (_currentTask != null)
		{
			_currentTask.Exit();
		}
		_currentTask = null;
		_currentTaskList.Clear();
		_currentIndex = 0;
	}
}
