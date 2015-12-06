using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Managers : Singleton<Managers>
{
	public LevelManager LevelManager;
	public WaveManager WaveManager;

	protected override void Awake()
	{
		base.Awake();

		CreateManager(LevelManager);
		CreateManager(WaveManager);
	}

	public Singleton<T> CreateManager<T>(Singleton<T> managerPrefab) where T : Singleton<T>
	{
		if (managerPrefab == null)
			return null;

		Singleton<T> manager = Singleton<T>.Find();

		if (manager == null)
		{
			manager = Instantiate(managerPrefab);
			manager.CachedTransform.parent = CachedTransform;
		}

		return manager;
	}

	public void DestroyManager<T>() where T : Singleton<T>
	{
		Singleton<T> manager = Singleton<T>.Find();

		if (manager != null)
			manager.Destroy();
	}

	protected virtual void Reset()
	{
		this.SetExecutionOrder(-2117);
	}
}
