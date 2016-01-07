using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class LaserAttack : AttackBase, IUpdateable
{
	public Color ActiveColor = Color.red;
	public Color InactiveColor = new Color(0.5f, 0f, 0f, 0.25f);
	public ParticleEffect Particles;

	int lastAttackFrame;
	bool isAttacking;

	public LaserRaycaster2D Raycaster;
	public LineRenderer LineRenderer;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public virtual void Update()
	{
		UpdateLine();
		Particles.CachedGameObject.SetActive(isAttacking);
		isAttacking = false;
	}

	public override void Attack()
	{
		if (Time.frameCount - lastAttackFrame > 1)
			Entity.SendMessage(EntityMessages.OnAttack);

		lastAttackFrame = Time.frameCount;
		isAttacking = true;
		Particles.CachedGameObject.SetActive(true);

		if (Raycaster.Hits.Count > 0)
		{
			var damager = Entity.GetComponent<DamagerBase>();
			var hit = Raycaster.Hits.Last();
			var entity = hit.collider.GetEntity();
			damager.Damage(entity == null ? null : entity.GetComponent<Damageable>());
		}
	}

	void UpdateLine()
	{
		Raycaster.Cast();
		LineRenderer.SetVertexCount(Raycaster.BounceCount + 2);
		LineRenderer.SetPosition(0, Entity.Transform.position);
		LineRenderer.material.color = isAttacking ? ActiveColor : InactiveColor;

		var endPosition = Raycaster.EndPosition;
		endPosition.z = Entity.Transform.position.z;

		for (int i = 0; i < Raycaster.BounceCount; i++)
		{
			var hit = Raycaster.Hits[i];
			LineRenderer.SetPosition(i + 1, hit.point);
		}

		LineRenderer.SetPosition(Raycaster.BounceCount + 1, endPosition);
		Particles.CachedTransform.position = endPosition;
		Particles.CachedTransform.rotation = Quaternion.Euler(0f, 0f, Raycaster.EndDirection.ToVector2().Angle() + 90f);
	}
}
