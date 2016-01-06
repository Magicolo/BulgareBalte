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
	int timeBeforeNextWave;

	public Text Text;

	public float UpdateRate { get { return 0f; } }

	public void Update()
	{
		if (WaveManager.Instance == null || WaveManager.Instance.WaveIsInProgress || WaveManager.Instance.TimeBeforeNextWave <= 0f)
			Text.enabled = false;
		else
		{
			Text.enabled = true;

			if (timeBeforeNextWave != (timeBeforeNextWave = Mathf.CeilToInt(WaveManager.Instance.TimeBeforeNextWave)))
				Text.text = "Wave " + (WaveManager.Instance.CurrentWaveIndex + 1) + " In " + timeBeforeNextWave;
		}
	}
}
