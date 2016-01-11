using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class LevelManager : Singleton<LevelManager>
{
	public AudioSettingsBase Music;

	int frameCount;
	IEntityGroup playerGroup = EntityManager.GetEntityGroup(EntityGroups.Player);

	protected override void Start()
	{
		base.Start();

		if (WaveManager.Instance != null)
			WaveManager.Instance.OnWavesCompleted += OnWavesCompleted;

		if (Music != null)
		{
			var item = AudioManager.Instance.CreateItem(Music);
			item.OnUpdate += i => { if (this == null) item.Stop(); };
			item.Play();
		}
	}

	void Update()
	{
		if (frameCount > 5 && playerGroup.Entities.Count == 0 && GameManager.Instance.CurrentState == GameManager.GameStates.Playing)
			GameManager.Instance.LevelFailure();

		frameCount++;
	}

	public void OnWavesCompleted()
	{
		GameManager.Instance.LevelSuccess();
	}
}
