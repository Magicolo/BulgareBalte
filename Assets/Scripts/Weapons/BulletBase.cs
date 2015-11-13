using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Copy]
public class BulletBase : PMonoBehaviour, ICopyable<BulletBase>, IClonable<BulletBase>
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
		Pools.BehaviourPool.Recycle(this);
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

	public BulletBase Clone()
	{
		return Pools.BehaviourPool.CreateCopy(this);
	}
}
