using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Player : CharacterBase
{
	public InputManager.Players Input;
	public WeaponBase StartWeapon;

	public bool IsMoving { get; private set; }
	public bool IsAiming { get; private set; }
	public bool IsFiring { get; private set; }

	Vector2 inputMotionDirection;
	Vector2 inputAimDirection;
	float nextAttackTime;

	readonly CachedValue<Rigidbody2D> cachedRigidbody2D;
	public Rigidbody2D CachedRigidbody2D { get { return cachedRigidbody2D; } }

	public Player()
	{
		cachedRigidbody2D = new CachedValue<Rigidbody2D>(GetComponent<Rigidbody2D>);
	}

	void Start()
	{
		if (StartWeapon != null)
			EquipWeapon(StartWeapon);
	}

	void Update()
	{
		UpdateInput();
		UpdateWeapon();
	}

	void FixedUpdate()
	{
		UpdateMotion();
		UpdateAim();
	}

	void UpdateInput()
	{
		inputMotionDirection.x = InputManager.Instance.GetAxis(Input, "MoveX");
		inputMotionDirection.y = InputManager.Instance.GetAxis(Input, "MoveY");
		inputMotionDirection = inputMotionDirection.ClampMagnitude(0f, 1f);
		IsMoving = Mathf.Abs(inputMotionDirection.x) + Mathf.Abs(inputMotionDirection.y) > 0f;

		inputAimDirection.x = InputManager.Instance.GetAxis(Input, "AimX");
		inputAimDirection.y = InputManager.Instance.GetAxis(Input, "AimY");
		inputAimDirection = inputAimDirection.ClampMagnitude(0f, 1f);
		IsAiming = Mathf.Abs(inputAimDirection.x) + Mathf.Abs(inputAimDirection.y) > 0f;

		IsFiring = InputManager.Instance.GetKey(Input, "Fire");
	}

	void UpdateWeapon()
	{
		if (IsFiring && CachedTime.Time > nextAttackTime && CurrentEquipment.Weapon != null)
		{
			CurrentEquipment.Weapon.Fire();
			nextAttackTime = CachedTime.Time + 1f / (Stats.AttackSpeed * CurrentEquipment.Weapon.AttackSpeedModifier);
		}
	}

	void UpdateMotion()
	{
		if (inputMotionDirection.sqrMagnitude <= 0f)
			return;

		CachedRigidbody2D.AddForce(inputMotionDirection * CurrentStats.MovementSpeed * CachedTime.FixedDeltaTime, ForceMode2D.Impulse);
	}

	void UpdateAim()
	{
		if (inputAimDirection.sqrMagnitude <= 0f)
			return;

		CachedRigidbody2D.RotateTowards(inputAimDirection.Angle(), CachedTime.DeltaTime);
	}

	public override void Kill()
	{

	}

	public override bool CanBeDamagedBy(DamageSources damageSource, DamageTypes damageType)
	{
		return true;
	}
}
