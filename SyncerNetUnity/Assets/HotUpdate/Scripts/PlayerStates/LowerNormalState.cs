using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerNormalState : PlayerState
{
	PlayerStateLayer stateLayer;
	PlayerStateMachine stateMachine;
	Animator animator;
	LocalPlayer player;

	Vector2 locomotion;
	float transitionSpeed = 0.1f;

	float xLimit = 0;
	float yLimit = 0;

	public override void Init(PlayerStateLayer stateLayer)
	{
		this.stateLayer = stateLayer;
		stateMachine = stateLayer.stateMachine;
		animator = stateMachine.gameObject.GetComponent<Animator>();
		player = stateMachine.gameObject.GetComponent<LocalPlayer>();
	}

	public override void OnStayState()
	{
		float x = Mathf.Abs(player.direction.x);
		float y = Mathf.Abs(player.direction.y);

		if (player.isSprint)
		{
			xLimit += (x - xLimit) * transitionSpeed;
			yLimit += (y - yLimit) * transitionSpeed;
		}
		else
		{
			xLimit += (x * 0.5f - xLimit) * transitionSpeed;
			yLimit += (y * 0.5f - yLimit) * transitionSpeed;
		}

		locomotion = new Vector3(Mathf.Clamp(locomotion.x + player.direction.x * 0.05f, -xLimit, xLimit), Mathf.Clamp(locomotion.y + player.direction.y * 0.05f, -yLimit, yLimit));

		if (Mathf.Abs(locomotion.x - (-2)) <= 0.1)
		{
			locomotion.x = -2;
		}
		else if (Mathf.Abs(locomotion.x - (-1)) <= 0.1)
		{
			locomotion.x = -1;
		}
		else if (Mathf.Abs(locomotion.x) <= 0.1)
		{
			locomotion.x = 0;
		}
		else if (Mathf.Abs(locomotion.x - 1) <= 0.1)
		{
			locomotion.x = 1;
		}
		else if (Mathf.Abs(locomotion.x - 2) <= 0.1)
		{
			locomotion.x = 2;
		}

		if (Mathf.Abs(locomotion.y - (-2)) <= 0.1)
		{
			locomotion.y = -2;
		}
		else if (Mathf.Abs(locomotion.y - (-1)) <= 0.1)
		{
			locomotion.y = -1;
		}
		else if (Mathf.Abs(locomotion.y) <= 0.1)
		{
			locomotion.y = 0;
		}
		else if (Mathf.Abs(locomotion.y - 1) <= 0.1)
		{
			locomotion.y = 1;
		}
		else if (Mathf.Abs(locomotion.y - 2) <= 0.1)
		{
			locomotion.y = 2;
		}

		animator.SetFloat("x", locomotion.x);
		animator.SetFloat("y", locomotion.y);

		if (locomotion == Vector2.zero)
		{
			player.isSpeedZero = true;
		}
		else
		{
			player.isSpeedZero = false;
		}
	}
}
