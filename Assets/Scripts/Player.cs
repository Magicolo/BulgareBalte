using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Player : PMonoBehaviour
{
	[Serializable]
	public class PlayerStats
	{
		public float MovementSpeed = 50f;
	}

	public PlayerStats Stats;
	public TimeComponent CachedTime { get { return cachedTime; } }
	public Rigidbody2D CachedRigidbody { get { return cachedRigidbody; } }

	CachedValue<TimeComponent> cachedTime;
	CachedValue<Rigidbody2D> cachedRigidbody;
	Vector2 inputDirection;

	public Player()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
		cachedRigidbody = new CachedValue<Rigidbody2D>(GetComponent<Rigidbody2D>);
	}

	void Update()
	{
		UpdateInput();
	}

	void FixedUpdate()
	{
		UpdateMotion();
	}

	void UpdateInput()
	{
		inputDirection.x = InputManager.Instance.GetAxis(InputManager.Players.Player1, "MoveX");
		inputDirection.y = InputManager.Instance.GetAxis(InputManager.Players.Player1, "MoveY");
		inputDirection = inputDirection.ClampMagnitude(0f, 1f);
	}

	void UpdateMotion()
	{
		CachedRigidbody.AddForce(inputDirection * Stats.MovementSpeed);
	}
}
