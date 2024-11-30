using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
	PlayerStateMachine stateMachine;
	Animator animator;
	LocalPlayer player;
	public override void Init(PlayerStateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
		animator = stateMachine.gameObject.GetComponent<Animator>();
		player = stateMachine.gameObject.GetComponent<LocalPlayer>();
	}
	public override void OnEnterState(string lastState)
	{
		animator.CrossFade("idle", 0.2f);
	}
	public override void OnStayState()
	{
		
		if (player.direction > 0)
		{
			stateMachine.ChangeState("Walking");
		}
		Camera.main.transform.eulerAngles = player.cameraRot;
		Camera.main.transform.position = player.transform.position + new Vector3(0, 3, 0) - Camera.main.transform.forward * 2;
	}
}
