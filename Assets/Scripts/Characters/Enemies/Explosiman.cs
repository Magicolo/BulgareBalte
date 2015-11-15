using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public class Explosiman : Enemy
{
	public float LifeTime = 5f;
	public float CheckInterval = 1f;
	[DoNotInitialize]
	public Explosion Explosion;

	Player target;
	float lifeCounter;
	float nextPlayerCheckTime;
	bool willExplode;

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
		CachedRigidbody.AddForce(direction * currentStats.MovementSpeed * CachedTime.FixedDeltaTime, ForceMode2D.Impulse);
	}

	protected override bool ShouldDie()
	{
		return base.ShouldDie() || lifeCounter <= 0f || willExplode;
	}

	public override void Kill()
	{
		ParticleManager.Instance.Create(Explosion, CachedTransform.position - new Vector3(0f, 0f, 0.2f));

		base.Kill();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<Player>() != null)
			willExplode = true;
	}

	public override void OnCreate()
	{
		base.OnCreate();

		lifeCounter = LifeTime;
	}
}
