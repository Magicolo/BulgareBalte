using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class LevelManager : Singleton<LevelManager>
{
	protected override void Start()
	{
		base.Start();

		if (WaveManager.Instance != null)
			WaveManager.Instance.OnWavesCompleted += OnWavesCompleted;
	}

	public void OnWavesCompleted()
	{

	}
}
