using System.Collections;

public abstract class PlayerState
{
	/// <summary>
	/// ״̬��ʼ��
	/// </summary>
	/// <param name="stateMachine"></param>
	public abstract void Init(PlayerStateMachine stateMachine);
	/// <summary>
	/// ����״̬ʱִ��
	/// </summary>
	/// <param name="lastState"></param>
	public virtual void OnEnterState(string lastState) {  }
	/// <summary>
	/// ����״ִ̬��
	/// </summary>
	public virtual void OnStayState() {  }
	/// <summary>
	/// �˳�״̬ʱִ��
	/// </summary>
	public virtual void OnExitState() {  }
}
