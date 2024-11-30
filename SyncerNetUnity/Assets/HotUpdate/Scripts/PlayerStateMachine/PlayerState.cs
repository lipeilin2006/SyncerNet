using System.Collections;

public abstract class PlayerState
{
	/// <summary>
	/// 状态初始化
	/// </summary>
	/// <param name="stateMachine"></param>
	public abstract void Init(PlayerStateMachine stateMachine);
	/// <summary>
	/// 进入状态时执行
	/// </summary>
	/// <param name="lastState"></param>
	public virtual void OnEnterState(string lastState) {  }
	/// <summary>
	/// 保持状态执行
	/// </summary>
	public virtual void OnStayState() {  }
	/// <summary>
	/// 退出状态时执行
	/// </summary>
	public virtual void OnExitState() {  }
}
