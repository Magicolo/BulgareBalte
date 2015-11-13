using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D)), Copy]
public class Explosiman : Enemy, ICopyable<Explosiman>
{
	public float LifeTime = 5f;
	public float CheckInterval = 1f;

	Player target;
	float lifeCounter;
	float nextPlayerCheckTime;

	readonly CachedValue<Rigidbody2D> cachedRigidbody;
	public Rigidbody2D CachedRigidbody { get { return cachedRigidbody; } }

	public Explosiman()
	{
		cachedRigidbody = new CachedValue<Rigidbody2D>(GetComponent<Rigidbody2D>);
	}

	void Update()
	{
		UpdateTarget();
	}

	void FixedUpdate()
	{
		UpdateMotion();
	}

	void UpdateTarget()
	{
		if (target == null || CachedTime.Time > nextPlayerCheckTime)
		{
			target = Player.GetClosest(CachedTransform.position);
			nextPlayerCheckTime = CachedTime.Time + CheckInterval;
		}
	}

	void UpdateMotion()
	{
		if (target == null)
			return;

		Vector3 direction = (target.CachedTransform.position - CachedTransform.position).normalized;
		CachedRigidbody.AddForce(direction * CurrentStats.MovementSpeed * CachedTime.FixedDeltaTime, ForceMode2D.Impulse);
	}

	protected override bool ShouldDie()
	{
		return base.ShouldDie() || lifeCounter <= 0f;
	}

	public override void Kill()
	{
		ParticleManager.Instance.Create("Explosion1", CachedTransform.position);

		base.Kill();
	}

	public override void OnCreate()
	{
		base.OnCreate();

		lifeCounter = LifeTime;
	}

	public void Copy(Explosiman reference)
	{
		base.Copy(reference);

		LifeTime = reference.LifeTime;
		CheckInterval = reference.CheckInterval;
		target = reference.target;
		lifeCounter = reference.lifeCounter;
		nextPlayerCheckTime = reference.nextPlayerCheckTime;
	}

	public override CharacterBase Clone()
	{
		return Pools.BehaviourPool.CreateCopy(this);
	}
}
