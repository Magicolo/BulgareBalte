using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using UnityEngine.UI;

public class UIBar : PMonoBehaviour
{
	public Slider Slider;
	public CanvasGroup Fader;
	public float FadeSpeed = 5f;

	public float Value { get { return Slider.value; } set { Slider.value = value; } }
	public bool Show { get; set; }

	protected virtual void Update()
	{
		Fader.alpha = Mathf.Lerp(Fader.alpha, Show ? 1f : 0f, TimeManager.UI.DeltaTime);
	}
}
