using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class EntityGroup : PComponent
{
	static readonly List<PEntity>[] groups;

	public enum Groups
	{
		None,
		Player,
		Enemy,
		Building,
		Environment
	}

	[SerializeField]
	Groups group;

	static EntityGroup()
	{
		groups = new List<PEntity>[Enum.GetValues(typeof(Groups)).Length];

		for (int i = 0; i < groups.Length; i++)
			groups[i] = new List<PEntity>();
	}

	public static List<PEntity> GetEntities(Groups group)
	{
		return groups[(int)group];
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

	public static void AddToGroup(Groups group, PEntity entity)
	{
		GetEntities(group).Add(entity);
	}

	public static void RemoveFromGroup(Groups group, PEntity entity)
	{
		GetEntities(group).Remove(entity);
	}

	public void SetGroup(Groups group)
	{
		RemoveFromGroup(group, Entity);
		this.group = group;
		AddToGroup(group, Entity);
	}

	public override void OnCreate()
	{
		base.OnCreate();

		AddToGroup(group, Entity);
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		RemoveFromGroup(group, Entity);
	}
}
