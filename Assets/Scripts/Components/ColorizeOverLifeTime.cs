using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("General"), EntityRequires(typeof(LifeTime))]
public class ColorizeOverLifeTime : ComponentBase, IUpdateable
{
	public SpriteRenderer Renderer;
	public Color StartColor = Color.white;
	public Color EndColor = Color.red;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public virtual void Update()
	{
		var time = Entity.GetComponent<LifeTime>();

		Renderer.color = Color.Lerp(StartColor, EndColor, time.Progress);
	}
}

