using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class UIHealthBar : UIBar
{
	public float HideDelay = 3f;

	CharacterBase character;
	float lastValue;
	float lastValueChangeTime;

	public virtual void Initialize(CharacterBase character)
	{
		this.character = character;
	}

	protected override void Update()
	{
		base.Update();

		Transform.position = character.Transform.position;
		Show = TimeManager.UI.Time > lastValueChangeTime + HideDelay;

		float value = character.CurrentStats.Health / character.Stats.Health;

		if (lastValue != value)
		{
			lastValue = value;
			lastValueChangeTime = TimeManager.UI.Time;
		}
	}
}
