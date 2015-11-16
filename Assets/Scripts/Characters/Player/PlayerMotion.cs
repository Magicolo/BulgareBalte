using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class PlayerMotion : MotionBase
{
	public InputManager.Players Input;

	Vector2 inputMotionDirection;
	Vector2 inputAimDirection;

	public override void UpdateMotion()
	{
		UpdateInput();
		base.UpdateMotion();
	}

	void UpdateInput()
	{
		inputMotionDirection.x = InputManager.Instance.GetAxis(Input, "MoveX");
		inputMotionDirection.y = InputManager.Instance.GetAxis(Input, "MoveY");
		inputMotionDirection = inputMotionDirection.ClampMagnitude(0f, 1f);

		inputAimDirection.x = InputManager.Instance.GetAxis(Input, "AimX");
		inputAimDirection.y = InputManager.Instance.GetAxis(Input, "AimY");
		inputAimDirection = inputAimDirection.ClampMagnitude(0f, 1f);
	}

	protected override bool ShouldMove()
	{
		return inputMotionDirection.sqrMagnitude > 0f;
	}

	protected override Vector2 GetDirection()
	{
		return inputMotionDirection;
	}

	protected override bool ShouldRotate()
	{
		return inputAimDirection.sqrMagnitude > 0f;
	}

	protected override float GetAngle()
	{
		return inputAimDirection.Angle();
	}
}
