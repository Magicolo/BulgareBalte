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

	[InitializeContent]
	public Animator Animator;

	bool willExplode;

	protected override bool ShouldDie()
	{
		return base.ShouldDie() || willExplode;
	}

	public override void Kill()
	{
		Animator.SetTrigger("Explose");
		//TODO faire un delay pour qu'on voit l'animation .
		Explosion explosion = ParticleManager.Instance.Create(Explosion, Transform.position - new Vector3(0f, 0f, 0.2f));
		explosion.Initialize(new DamageData(currentStats.Damage, currentStats.DamageSource));

		base.Kill();
	}

	public override void OnCreate()
	{
		base.OnCreate();

		Animator.SetTrigger("Spawn");
		Debug.Log("Boby");
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<Player>() != null)
			willExplode = true;
	}
}
