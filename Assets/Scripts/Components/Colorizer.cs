using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class Colorizer : ComponentBase, IUpdateable
{
	public SpriteRenderer Renderer;
	public float FadeSpeed = 5f;
	public Color Normal = Color.white;
	public Color Damaged = Color.red;

	Color currentColor = Color.white;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public virtual void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();

		currentColor = Color.Lerp(currentColor, Normal, FadeSpeed * time.DeltaTime);
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
