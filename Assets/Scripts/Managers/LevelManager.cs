using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class LevelManager : Singleton<LevelManager>
{
	IEntityGroup playerGroup = EntityManager.GetEntityGroup(EntityGroups.Player);

	void Update()
	{
		if (playerGroup.Entities.Count == 0 && GameManager.Instance.CurrentState == GameManager.GameStates.Playing)
			GameManager.Instance.LevelFailure();
	}

	protected override void Start()
	{
		base.Start();

		if (WaveManager.Instance != null)
			WaveManager.Instance.OnWavesCompleted += OnWavesCompleted;
	}

	public void OnWavesCompleted()
	{
		GameManager.Instance.LevelSuccess();
	}
}
