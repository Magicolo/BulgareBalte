using UnityEngine;
using System.Collections.Generic;
using Pseudo;

public class GamePersistentManagers : PersistentManagers
{
	public GameManager GameManager;

	protected override void Awake()
	{
		if (Instance != null)
		{
			CachedGameObject.Destroy();
			return;
		}

		base.Awake();

		CreateManager(GameManager);
	}
}
