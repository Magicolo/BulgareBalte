using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public class Explosiman : SeekerEnemy
{
	public float LifeTime = 5f;
	[InitializeContent]
	public Explosion Explosion;

	float lifeCounter;
	bool willExplode;
	float randomAmplitude;
	float randomFrequency;
	float randomOffset;

	protected override float GetAngle()
	{
		return base.GetAngle() + randomAmplitude * Mathf.Sin(CachedTime.Time * randomFrequency + randomOffset);
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
		randomAmplitude = PRandom.Range(25f, 100f);
		randomFrequency = PRandom.Range(1f, 5f);
		randomOffset = PRandom.Range(0f, 1000f);
	}
}
