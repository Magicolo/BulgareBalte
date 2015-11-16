using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : PMonoBehaviour
{
	public float DamageModifier = 1f;
	public float LifeTime = 1f;
	[InitializeContent]
	public MotionBase Motion;
	[DoNotInitialize]
	public Explosion KillEffect;

	protected float lifeCounter;
	protected DamageData damage;

	public void Initialize(DamageData damage)
	{
		damage.Damage *= DamageModifier;
		this.damage = damage;
	}

	protected virtual void LateUpdate()
	{
		UpdateLifeTime();
	}

	protected virtual void UpdateLifeTime()
	{
		lifeCounter -= TimeManager.Player.DeltaTime;

		if (lifeCounter <= 0f)
			Kill();
	}

	public virtual void Kill()
	{
		if (KillEffect != null)
		{
			Explosion explosion = ParticleManager.Instance.Create(KillEffect, CachedTransform.position);
			explosion.Initialize(damage);
		}

		PoolManager.Recycle(this);
	}

	public override void OnCreate()
	{
		base.OnCreate();

		lifeCounter = LifeTime;
	}

	void OnTriggerEnter2D()
	{
		Kill();
	}
}
