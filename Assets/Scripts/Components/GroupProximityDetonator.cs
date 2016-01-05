using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class GroupProximityDetonator : ComponentBase
{
	public EntityMatch Group;
	public float Radius = 1f;
	public ParticleEffect Explosion;

	protected virtual void Update()
	{
		var entities = EntityManager.GetEntityGroup(Group).Entities;

		for (int i = 0; i < entities.Count; i++)
		{
			var entity = entities[i];

			if (Vector2.Distance(entity.Transform.position, Entity.Transform.position) <= Radius)
			{
				Entity.SendMessage(EntityMessages.OnDie);
				return;
			}
		}
	}

	protected virtual void OnDie()
	{
		ParticleManager.Instance.Create(Explosion, Entity.Transform.position - new Vector3(0f, 0f, 0.2f));
	}
}
