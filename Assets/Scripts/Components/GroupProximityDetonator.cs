using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class GroupProximityDetonator : PComponent
{
	public EntityMatch Group;
	public float Radius = 1f;
	public ParticleEffect Explosion;

	protected virtual void Update()
	{
		var entities = EntityManager.GetEntities(Group);

		for (int i = 0; i < entities.Count; i++)
		{
			var entity = entities[i];

			if (Vector2.Distance(entity.CachedTransform.position, CachedTransform.position) <= Radius)
			{
				SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
				return;
			}
		}
	}

	protected virtual void OnDie()
	{
		ParticleManager.Instance.Create(Explosion, CachedTransform.position - new Vector3(0f, 0f, 0.2f));
	}
}
