using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public class Explosiman : Enemy
{
	[InitializeContent]
	public Explosion Explosion;

	bool willExplode;

	protected override bool ShouldDie()
	{
		return base.ShouldDie() || willExplode;
	}

	public override void Kill()
	{
		Explosion explosion = ParticleManager.Instance.Create(Explosion, CachedTransform.position - new Vector3(0f, 0f, 0.2f));
		explosion.Initialize(new DamageData(currentStats.Damage, currentStats.DamageSource));

		base.Kill();
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<Player>() != null)
			willExplode = true;
	}
}
