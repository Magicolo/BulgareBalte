using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class CharacterSpawner : SpawnerBase<CharacterBase>
{
	[Min]
	public float SpawnInterval;
	public CharacterBase ToSpawn;

	float nextSpawnTime;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public CharacterSpawner()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	public override CharacterBase Spawn()
	{
		CharacterBase character = PoolManager.Create(ToSpawn);
		character.CachedTransform.position = CachedTransform.position;

		nextSpawnTime = CachedTime.Time + SpawnInterval;

		return character;
	}

	protected override bool ShouldSpawn()
	{
		return CachedTime.Time >= nextSpawnTime;
	}
}
