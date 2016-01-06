using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class WaveManager : Singleton<WaveManager>
{
	public event Action<Wave> OnWaveStarted;
	public event Action<Wave> OnWaveCompleted;
	public event Action OnWavesCompleted;

	public float NextWaveDelay = 3f;
	public Wave[] Waves;
	public Wave CurrentWave { get; private set; }
	public int CurrentWaveIndex { get { return Waves.Length - queuedWaves.Count; } }
	public float TimeBeforeNextWave { get { return nextWaveCounter; } }
	public bool WaveIsInProgress
	{
		get { return waveIsInProgress; }
		set
		{
			if (waveIsInProgress != value)
			{
				waveIsInProgress = value;

				if (waveIsInProgress)
					RaiseOnWaveStartedEvent(CurrentWave);
				else
					RaiseOnWaveCompletedEvent(CurrentWave);
			}
		}
	}

	Queue<Wave> queuedWaves = new Queue<Wave>();
	IEntityGroup enemyGroup = EntityManager.GetEntityGroup(EntityGroups.Enemy);
	float nextWaveCounter;
	bool waveIsInProgress;

	protected override void Awake()
	{
		base.Awake();

		ResetWaves();
	}

	void Update()
	{
		UpdateWaves();
	}

	protected virtual void UpdateWaves()
	{
		WaveIsInProgress = CurrentWave != null && !CurrentWave.IsCompleted || enemyGroup.Entities.Count > 0;

		if (waveIsInProgress)
			return;

		if (CurrentWave != null && queuedWaves.Count == 0)
		{
			CurrentWave = null;
			RaiseOnWavesCompletedEvent();
		}
		else if (nextWaveCounter <= 0f)
			NextWave();
		else
			nextWaveCounter -= TimeManager.World.DeltaTime;
	}

	void NextWave()
	{
		if (queuedWaves.Count > 0)
		{
			var nextWave = queuedWaves.Dequeue();
			SwitchWave(nextWave);
		}

		WaveIsInProgress = true;
		nextWaveCounter = queuedWaves.Count > 0 ? NextWaveDelay : 0f;
	}

	void SwitchWave(Wave wave)
	{
		if (CurrentWave != null)
			CurrentWave.CachedGameObject.SetActive(false);

		CurrentWave = wave;
		CurrentWave.CachedGameObject.SetActive(true);
	}

	void ResetWaves()
	{
		waveIsInProgress = false;
		nextWaveCounter = NextWaveDelay;
		queuedWaves.Clear();

		for (int i = 0; i < Waves.Length; i++)
			queuedWaves.Enqueue(Waves[i]);
	}

	protected virtual void RaiseOnWaveStartedEvent(Wave wave)
	{
		if (OnWaveStarted != null)
			OnWaveStarted(wave);
	}

	protected virtual void RaiseOnWaveCompletedEvent(Wave wave)
	{
		if (OnWaveCompleted != null)
			OnWaveCompleted(wave);
	}

	protected virtual void RaiseOnWavesCompletedEvent()
	{
		if (OnWavesCompleted != null)
			OnWavesCompleted();
	}
}
