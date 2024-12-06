using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色状态机
/// </summary>
public class PlayerStateMachine : MonoBehaviour
{
	public List<PlayerStateLayer> Layers { get; private set; } = new List<PlayerStateLayer>();

	public void AddLayer(PlayerStateLayer layer)
	{
		layer.stateMachine = this;
		Layers.Add(layer);
	}

	public void FixedUpdate()
	{
		foreach(var layer in Layers)
		{
			layer.FixedUpdate();
		}
	}

}
