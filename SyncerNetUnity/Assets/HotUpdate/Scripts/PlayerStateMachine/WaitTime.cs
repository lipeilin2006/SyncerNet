using UnityEngine;

/// <summary>
/// ���ڵȴ�ʱ�䣬���״̬��ʹ��
/// </summary>
public class WaitTime
{
	private float t = 0;
	private float waitTime = 0;

	public bool isFinished { get { return t >= waitTime; } }

	public bool isStarted { get; private set; } = false;
	/// <summary>
	/// ʵ��������
	/// </summary>
	/// <param name="waitTime">�ȴ���ʱ������λ����</param>
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