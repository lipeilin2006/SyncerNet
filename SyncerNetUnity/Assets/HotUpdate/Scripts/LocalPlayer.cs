using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalPlayer : MonoBehaviour
{
	[HideInInspector]
	public float direction;

	[HideInInspector]
	public Vector2 cameraRot;

	public float camSpeedX = 1.0f;
	public float camSpeedY = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
		PlayerStateMachine stateMachine = GetComponent<PlayerStateMachine>();
		stateMachine.AddState("Idle", new IdleState());
		stateMachine.AddState("Walking", new WalkingState());
		stateMachine.Init("Idle");
	}

    // Update is called once per frame
    void Update()
    {
		
    }

	private void FixedUpdate()
	{
		Camera.main.transform.eulerAngles = cameraRot;
		Camera.main.transform.position = this.transform.position + new Vector3(0, 3, 0) - Camera.main.transform.forward * 2;
	}

	public void OnWS(InputValue inputValue)
	{
		direction = inputValue.Get<float>();
	}

	public void OnMouseMove(InputValue inputValue)
	{
		Vector2 delta = inputValue.Get<Vector2>();

		cameraRot.x = Mathf.Clamp(cameraRot.x - delta.y * camSpeedY, -60, 60);
		cameraRot.y += delta.x * camSpeedX;

		if (cameraRot.y > 180)
		{
			cameraRot.y -= 360;
		}
		else if (cameraRot.y < -180)
		{
			cameraRot.y += 360;
		}
	}
}
