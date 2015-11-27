using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class GroupProximityDetonator : PComponent
{
	public EntityGroup.Groups Group;
	public float Radius = 1f;
	public ParticleEffect Explosion;

	protected virtual void Update()
	{
		var entities = EntityGroup.GetEntities(Group);

		for (int i = 0; i < entities.Count; i++)
		{
			var entity = entities[i];

			if (Vector2.Distance(entity.Transform.position, Transform.position) <= Radius)
			{
				SendMessage("OnDie", SendMessageOptions.DontRequireReceiver);
				return;
			}
		}
	}

	protected virtual void OnDie()
	{
		ParticleManager.Instance.Create(Explosion, Transform.position - new Vector3(0f, 0f, 0.2f));
	}
}
