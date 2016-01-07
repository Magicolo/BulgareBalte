using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("General")]
public class RecycleOnAnimatorState : ComponentBase, ILateUpdateable
{
	public string EnterAnimationName;
	public string ExitAnimationName;

	bool shouldRecycle;

	public float LateUpdateRate
	{
		get { return 0f; }
	}

	public void LateUpdate()
	{
		if (shouldRecycle)
			PrefabPoolManager.Recycle(Entity);
	}

	void OnStateEnter(AnimatorStateInfo info)
	{
		if (info.IsName(EnterAnimationName))
		{
			shouldRecycle = true;
		}
	}

	void OnStateExit(AnimatorStateInfo info)
	{
		if (info.IsName(ExitAnimationName))
		{
			shouldRecycle = true;
		}
	}
}

