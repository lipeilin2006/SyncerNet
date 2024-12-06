using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperAimState : PlayerState
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
		animator.CrossFade("Aim", 0.02f, 1);
		player.isAim = true;
	}
	public override void OnStayState()
	{
		animator.SetFloat("lookX", player.cameraRot.x);
		if (!player.isAim)
		{
			stateLayer.ChangeState("Idle");
		}
	}
	public override void OnExitState()
	{
		player.isAim = false;
	}
}
