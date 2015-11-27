using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class EntityGroup : PComponent
{
	static readonly Dictionary<int, List<PEntity>> groups = new Dictionary<int, List<PEntity>>();

	public enum Groups
	{
		Player,
		Enemy,
		Building,
		Environment
	}

	[SerializeField]
	Groups group = Groups.Enemy;

	public static List<PEntity> GetEntities(Groups group)
	{
		List<PEntity> entities;

		if (!groups.TryGetValue((int)group, out entities))
		{
			entities = new List<PEntity>();
			groups[(int)group] = entities;
		}

		return entities;
	}

	public static PEntity GetClosestEntity(Groups group, Vector3 position)
	{
		PEntity closest = null;
		float closestDistance = float.MaxValue;
		var entities = GetEntities(group);

		for (int i = 0; i < entities.Count; i++)
		{
			var entity = entities[i];
			float distance = (entity.Transform.position - position).sqrMagnitude;

			if (distance < closestDistance)
			{
				closest = entity;
				closestDistance = distance;
			}
		}

		return closest;
	}

	public void SetGroup(Groups group)
	{
		GetEntities(this.group).Remove(Entity);
		this.group = group;
		GetEntities(this.group).Add(Entity);
	}

	public override void OnCreate()
	{
		base.OnCreate();

		GetEntities(group).Add(Entity);
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		GetEntities(group).Remove(Entity);
	}
}
