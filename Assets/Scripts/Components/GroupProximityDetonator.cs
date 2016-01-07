using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("Attack")]
public class GroupProximityDetonator : ComponentBase, IUpdateable
{
	public EntityMatch Group;
	public float Radius = 1f;

	public Animator Animator;
	public string AnimationName;
	public string TriggerName;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public virtual void Update()
	{
		var entities = EntityManager.GetEntityGroup(Group).Entities;

		for (int i = 0; i < entities.Count; i++)
		{
			var entity = entities[i];

			if (Vector2.Distance(entity.Transform.position, Entity.Transform.position) <= Radius)
			{
				if (Animator)
					Animator.SetTrigger(TriggerName);
				else
					Die();
				return;
			}
		}
	}

	void OnStateExit(AnimatorStateInfo info)
	{
		if (info.IsName(AnimationName))
		{
			Die();
		}
	}

	private void Die()
	{
		DamageData data = new DamageData(Entity.GetComponent<Status>().Health, Entity.Groups, DamageTypes.None);
		Entity.GetComponent<Damageable>().Damage(data);
	}
}
