using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : PlayerState
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
		animator.CrossFade("walking", 0.2f);
	}

	public override void OnStayState()
	{
		if (player.direction == 0)
		{
			stateMachine.ChangeState("Idle");
		}
		Camera.main.transform.eulerAngles = player.cameraRot;
		player.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
		Camera.main.transform.position = player.transform.position + new Vector3(0, 3, 0) - Camera.main.transform.forward * 2;
	}
}
