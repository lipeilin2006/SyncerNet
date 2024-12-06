using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateLayer
{
	public PlayerStateMachine stateMachine;
	public Dictionary<string, PlayerState> States { get; private set; } = new Dictionary<string, PlayerState>();
	public string CurrentState { get; private set; }

	public bool isCrossing = false;

	public bool isUpdateFinished { get; private set; } = true;

	public bool isInit { get; private set; } = false;

	/// <summary>
	/// 添加新的状态
	/// </summary>
	/// <param name="name">状态名称</param>
	/// <param name="state">状态</param>
	public void AddState(string name, PlayerState state)
	{
		state.Init(this);
		States.Add(name, state);
	}

	/// <summary>
	/// 转换至指定状态
	/// </summary>
	/// <param name="name">状态名称</param>
	public void ChangeState(string name)
	{
		isCrossing = true;
		Debug.Log($"Change State form {CurrentState} to {name}");
		States[CurrentState].OnExitState();
		string last = CurrentState;
		CurrentState = name;
		States[CurrentState].OnEnterState(last);
		isUpdateFinished = true;
		isCrossing = false;
	}

	/// <summary>
	/// 在每个物理帧调用状态OnStayState函数
	/// </summary>
	public void FixedUpdate()
	{
		if (isInit)//判断状态机是否初始化
		{
			if (!isCrossing && isUpdateFinished)//判断是否正在过渡或上一次更新是否已完成
			{
				OnStayState();
			}
		}
	}

	private void OnStayState()
	{
		isUpdateFinished = false;
		States[CurrentState].OnStayState();
		isUpdateFinished = true;
	}
	/// <summary>
	/// 初始化状态机
	/// </summary>
	/// <param name="name">需要进入的初始状态</param>
	public void Init(string name)
	{
		Debug.Log("Init Player State Machine");
		CurrentState = name;
		States[CurrentState].OnEnterState(CurrentState);
		isInit = true;
	}
}
