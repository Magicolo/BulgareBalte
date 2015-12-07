using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WaveCounterUI : PComponent
{
	int timeBeforeNextWave;

	readonly CachedValue<Text> cachedText;
	public Text CachedText { get { return cachedText.Value; } }

	public WaveCounterUI()
	{
		cachedText = new CachedValue<Text>(GetComponent<Text>);
	}

	void Update()
	{
		if (WaveManager.Instance == null || WaveManager.Instance.WaveIsInProgress || WaveManager.Instance.TimeBeforeNextWave <= 0f)
			CachedText.enabled = false;
		else
		{
			CachedText.enabled = true;

			if (timeBeforeNextWave != (timeBeforeNextWave = Mathf.CeilToInt(WaveManager.Instance.TimeBeforeNextWave)))
				CachedText.text = "Wave " + (WaveManager.Instance.CurrentWaveIndex + 1) + " In " + timeBeforeNextWave;
		}
	}
}
