using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent), typeof(Rigidbody2D))]
public abstract class CharacterBase : PMonoBehaviour, IDamageable
{
	[InitializeContent]
	public CharacterStats Stats;
	[InitializeContent]
	public MotionBase Motion;

	[DoNotInitialize]
	public SpriteRenderer Renderer;
	[DoNotInitialize]
	public Transform WeaponRoot;

	public Color NormalColor = Color.white;
	public Color DamagedColor = Color.red;

	public CharacterStats CurrentStats { get { return currentStats; } }
	public CharacterEquipment CurrentEquipment { get { return currentEquipment; } }

	protected readonly CharacterStats currentStats = new CharacterStats();
	protected readonly CharacterEquipment currentEquipment = new CharacterEquipment();
	protected Color currentColor;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	readonly CachedValue<Rigidbody2D> cachedRigidbody;
	public Rigidbody2D CachedRigidbody { get { return cachedRigidbody; } }

	protected CharacterBase()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
		cachedRigidbody = new CachedValue<Rigidbody2D>(GetComponent<Rigidbody2D>);
	}

	protected virtual void LateUpdate()
	{
		UpdateStatus();
	}

	protected virtual void UpdateStatus()
	{
		UpdateColor();

		if (ShouldDie())
			Kill();
	}

	protected virtual void UpdateColor()
	{
		currentColor = Color.Lerp(currentColor, NormalColor, CachedTime.DeltaTime * 3f);
		Renderer.color = currentColor;
	}

	protected virtual bool ShouldDie()
	{
		return currentStats.Health <= 0;
	}

	public virtual void EquipWeapon(WeaponBase weaponPrefab)
	{
		UnequipWeapon();

		if (weaponPrefab == null)
			return;

		WeaponBase weapon = PoolManager.Create(weaponPrefab);
		weapon.CachedTransform.parent = WeaponRoot;
		weapon.CachedTransform.Copy(weaponPrefab.CachedTransform);

		currentEquipment.Weapon = weapon;
	}

	public virtual void UnequipWeapon()
	{
		PoolManager.Recycle(currentEquipment.Weapon);
		currentEquipment.Weapon = null;
	}

	public abstract void Kill();

	public abstract bool CanBeDamagedBy(DamageData damage);

	public virtual void Damage(DamageData damage)
	{
		currentStats.Health -= damage.Damage;
		currentColor = DamagedColor;
	}

	public override void OnCreate()
	{
		base.OnCreate();

		Motion.OnCreate();
		CachedTime.OnCreate();

		currentColor = NormalColor;
		currentStats.Copy(Stats);
		currentEquipment.Copy(CharacterEquipment.Default);
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		Motion.OnRecycle();
		CachedTime.OnRecycle();
	}
}
