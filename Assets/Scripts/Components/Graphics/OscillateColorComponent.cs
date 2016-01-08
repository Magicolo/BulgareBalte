using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Graphics"), RequireComponent(typeof(TimeComponent))]
public class OscillateColorComponent : ComponentBase, IUpdateable
{
	public Renderer Renderer;
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
		Renderer.OscillateColor(Frequency, Amplitude, Center, t, false, Channels);
	}
}

