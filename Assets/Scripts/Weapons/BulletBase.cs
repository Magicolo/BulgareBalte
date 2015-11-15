using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class BulletBase : PMonoBehaviour
{
	public float LifeTime = 1f;

	public WeaponBase Owner { get; set; }

	protected float lifeCounter;

	protected virtual void LateUpdate()
	{
		UpdateLifeTime();
	}

	void UpdateLifeTime()
	{
		lifeCounter -= TimeManager.Player.DeltaTime;

		if (lifeCounter <= 0f)
			Kill();
	}

	public void Kill()
	{
		PoolManager.Recycle(this);
	}

	public override void OnCreate()
	{
		base.OnCreate();

		lifeCounter = LifeTime;
	}

	public void Copy(BulletBase reference)
	{
		LifeTime = reference.LifeTime;
		lifeCounter = reference.lifeCounter;
		Owner = reference.Owner;
	}
}
