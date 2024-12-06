using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalPlayer : MonoBehaviour
{
	[HideInInspector]
	public Vector2 direction;

	[HideInInspector]
	public Vector2 cameraRot;

	[HideInInspector]
	public bool isSprint = false;

	[HideInInspector]
	public bool isAim = false;

	[HideInInspector]
	public bool isSpeedZero = false;

	public float camSpeedX = 1.0f;
	public float camSpeedY = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
		PlayerStateMachine stateMachine = GetComponent<PlayerStateMachine>();

		PlayerStateLayer upperLayer = new PlayerStateLayer();
		PlayerStateLayer lowerLayer = new PlayerStateLayer();
		stateMachine.AddLayer(upperLayer);
		stateMachine.AddLayer(lowerLayer);

		upperLayer.AddState("Idle", new UpperIdleState());
		upperLayer.AddState("Aim", new UpperAimState());

		lowerLayer.AddState("Normal", new LowerNormalState());

		upperLayer.Init("Idle");
		lowerLayer.Init("Normal");
	}

    // Update is called once per frame
    void Update()
    {
		
    }

	private void FixedUpdate()
	{
	}

	private void LateUpdate()
	{
		Camera.main.transform.eulerAngles = cameraRot;
		Camera.main.transform.position = transform.position + new Vector3(0, 1.2f, 0) - Camera.main.transform.forward * 1;
		if (isAim || !isSpeedZero)
		{
			transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
		}
	}

	public void OnMove(InputValue inputValue)
	{
		direction = inputValue.Get<Vector2>();
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

	public void OnShift(InputValue inputValue)
	{
		isSprint = !isSprint;
	}

	public void OnAim(InputValue inputValue)
	{
		isAim = !isAim;
	}
}
