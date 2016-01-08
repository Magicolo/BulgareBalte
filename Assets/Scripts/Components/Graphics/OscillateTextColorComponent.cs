using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;
using UnityEngine.UI;

[Serializable, ComponentCategory("Graphics"), RequireComponent(typeof(TimeComponent))]
public class OscillateTextColorComponent : ComponentBase, IUpdateable
{
	public Text Text;
	public float Frequency;
	public float Amplitude;
	public float Center;
	public Channels Channels = Channels.RGBA;

	float t;

	public float UpdateRate { get { return 0; } }

	public void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();
		t += time.DeltaTime;
		Text.color = Text.color.Oscillate(Frequency, Amplitude, Center, 0f, t, Channels);
	}
}

