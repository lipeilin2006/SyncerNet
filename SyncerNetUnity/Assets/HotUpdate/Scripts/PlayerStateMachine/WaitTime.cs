using UnityEngine;

/// <summary>
/// 用于等待时间，配合状态机使用
/// </summary>
public class WaitTime
{
	private float t = 0;
	private float waitTime = 0;

	public bool isFinished { get { return t >= waitTime; } }

	public bool isStarted { get; private set; } = false;
	/// <summary>
	/// WaitTime
	/// </summary>
	/// <param name="waitTime">等待的时长，单位：秒</param>
	public WaitTime(float waitTime)
	{
		this.waitTime = waitTime;
	}
	public void Start()
	{
		isStarted = true;
	}
	public void Tick()
	{
        if (isStarted)
        {
			t = Time.deltaTime;
		}
	}
	public void FixedTick()
	{
		if (isStarted)
		{
			t += Time.fixedDeltaTime;
		}
	}
	public void Reset()
	{
		t = 0;
		isStarted = false;
	}
}
