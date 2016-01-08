using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using UnityEngine.UI;

[Serializable, ComponentCategory("UI")]
public class WaveCounterUI : ComponentBase, IUpdateable
{
	public Text Text;
	public AudioSettingsBase CounterSound;
	public AudioSettingsBase NextWaveSound;

	public float UpdateRate { get { return 0f; } }

	int timeBeforeNextWave;

	public void Update()
	{
		if (WaveManager.Instance == null || WaveManager.Instance.WaveIsInProgress || WaveManager.Instance.TimeBeforeNextWave <= 0f)
			Text.enabled = false;
		else
		{
			if (!Text.enabled)
				AudioManager.Instance.CreateItem(NextWaveSound).Play();

			Text.enabled = true;

			if (timeBeforeNextWave != (timeBeforeNextWave = Mathf.CeilToInt(WaveManager.Instance.TimeBeforeNextWave)))
			{
				Text.text = "Wave " + (WaveManager.Instance.CurrentWaveIndex + 1) + " In " + timeBeforeNextWave;
				AudioManager.Instance.CreateItem(CounterSound).Play();
			}
		}
	}
}
