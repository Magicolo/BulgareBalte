using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Animator")]
public class AnimateOnDie : ComponentBase
{

	public Animator Animator;
	public string AnimationName;
	public string TriggerName;

	public GameObject DeadPrefab;

	public float UpdateRate { get { return 0; } }

	protected virtual void OnDie()
	{
		if (Animator)
		{
			Animator.SetTrigger(TriggerName);
			foreach (var item in Entity.GetComponents<DamagerBase>())
				item.Active = false;
			foreach (var item in Entity.GetComponents<AttackBase>())
				item.Active = false;
			foreach (var item in Entity.GetComponents<MotionBase>())
				item.Active = false;
		}
		else
			SpawnPrefab();
	}

	void OnStateExit(AnimatorStateInfo info)
	{
		if (info.IsName(AnimationName))
			SpawnPrefab();
	}

	private void SpawnPrefab()
	{
		if (DeadPrefab == null) return;

		var go = PrefabPoolManager.Create(DeadPrefab);
		go.transform.position = Entity.GameObject.transform.position;
		go.transform.localRotation = Entity.GameObject.transform.localRotation;
	}
}

