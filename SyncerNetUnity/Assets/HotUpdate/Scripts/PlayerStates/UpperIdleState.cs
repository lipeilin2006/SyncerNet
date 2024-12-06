using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperIdleState : PlayerState
{
	PlayerStateLayer stateLayer;
	PlayerStateMachine stateMachine;
	Animator animator;
	LocalPlayer player;

	public override void Init(PlayerStateLayer stateLayer)
	{
		this.stateLayer = stateLayer;
		stateMachine = stateLayer.stateMachine;
		animator = stateMachine.gameObject.GetComponent<Animator>();
		player = stateMachine.gameObject.GetComponent<LocalPlayer>();
	}

	public override void OnEnterState(string lastState)
	{
		animator.CrossFade("Idle", 0.02f, 1);
		animator.SetFloat("lookX", 0);
	}

	public override void OnStayState()
	{
		if (player.isAim)
		{
			stateLayer.ChangeState("Aim");
		}
	}
}
