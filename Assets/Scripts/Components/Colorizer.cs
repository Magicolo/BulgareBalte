using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class Colorizer : PComponent
{
	public SpriteRenderer Renderer;
	public float FadeSpeed = 5f;
	public Color Normal = Color.white;
	public Color Damaged = Color.red;

	Color currentColor = Color.white;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public Colorizer()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	protected virtual void Update()
	{
		currentColor = Color.Lerp(currentColor, Normal, FadeSpeed * CachedTime.DeltaTime);
		Renderer.color = currentColor;
	}

	protected virtual void OnDamaged(DamageData damage)
	{
		currentColor = Damaged;
	}

	public override void OnCreate()
	{
		base.OnCreate();

		currentColor = Color.white;
	}
}
