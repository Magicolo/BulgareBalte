using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class BurstLeveledSpawner : SpawnerBase, IUpdateable
{
	public PEntity ToSpawn;

	[Disable]
	public int currentLevel = -1;

	public int[] NbSpawned;
	public int[] Interval;

	float nextSpawnTime;
	int spawnRemainning;


	[SerializeField, Button]
	protected bool spawn;

	public override void Spawn()
	{
		currentLevel++;
		spawnRemainning = NbSpawned[currentLevel];
		nextSpawnTime = Entity.GetComponent<TimeComponent>().Time + Interval[currentLevel];
	}


	public float UpdateRate { get { return 0f; } }

	public virtual void Update()
	{
		if (ShouldSpawn())
			RealSpawn();
	}

	protected virtual bool ShouldSpawn()
	{
		return spawnRemainning > 0 && Entity.GetComponent<TimeComponent>().Time >= nextSpawnTime;
	}

	void RealSpawn()
	{
		nextSpawnTime = Entity.GetComponent<TimeComponent>().Time + Interval[currentLevel];
		spawnRemainning--;
		var spawned = PrefabPoolManager.Create(ToSpawn);
		spawned.CachedTransform.position = Entity.Transform.position;
	}
}

