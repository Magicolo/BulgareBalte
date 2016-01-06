using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class InputMotion : MotionBase
{
	public InputManager.Players Input;

	Vector2 inputMotion;
	Vector2 inputRotation;

	protected override void UpdateMotion()
	{
		UpdateInput();
		base.UpdateMotion();
	}

	protected virtual void UpdateInput()
	{
		var screenPosition = Camera.main.WorldToScreenPoint(Entity.Transform.position);

		inputMotion.x = InputManager.Instance.GetAxis(Input, "MoveX", screenPosition);
		inputMotion.y = InputManager.Instance.GetAxis(Input, "MoveY", screenPosition);
		inputMotion = inputMotion.ClampMagnitude(0f, 1f);

		inputRotation.x = InputManager.Instance.GetAxis(Input, "RotateX", screenPosition);
		inputRotation.y = InputManager.Instance.GetAxis(Input, "RotateY", screenPosition);
		inputRotation = inputRotation.ClampMagnitude(0f, 1f);
	}

	protected override Vector2 GetDirection()
	{
		return base.GetDirection() + inputMotion;
	}

	protected override float GetAngle()
	{
		return base.GetAngle() + inputRotation.Angle();
	}

	protected override bool ShouldMove()
	{
		return inputMotion.sqrMagnitude > 0f;
	}

	protected override bool ShouldRotate()
	{
		return inputRotation.sqrMagnitude > 0f;
	}
}
