using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using Pseudo.Internal.Physics;

public class LaserRaycaster2D : Raycaster2DBase
{
	public float Distance = 1000f;
	public int Bounces = 10;

	public int BounceCount { get; private set; }
	public Vector3 EndDirection { get; private set; }
	public Vector3 EndPosition { get; private set; }

	public override bool Cast()
	{
		Hits.Clear();
		Vector3 position = CachedTransform.position;
		Vector3 direction = CachedTransform.rotation * Vector3.right;
		BounceCount = 0;
		IReflector reflector;

		do
		{
			reflector = null;
			RaycastHit2D hit = Physics2D.Raycast(position, direction, Distance, Mask);

			if (hit.collider == null)
			{
				EndPosition = position + direction * Distance;
				DrawLine(position, EndPosition);
			}
			else
			{
				DrawLine(position, hit.point);
				reflector = hit.collider.GetComponentInParent<IReflector>();
				EndPosition = hit.point;

				if (reflector != null)
				{
					position = hit.point;
					direction = Vector3.Reflect(direction, hit.normal);
					BounceCount++;
				}

				Hits.Add(hit);
			}
		}
		while (BounceCount < Bounces && reflector != null);

		EndDirection = direction;

		return Hits.Count > 0;
	}

	void DrawLine(Vector3 startPosition, Vector3 endPosition)
	{
		if (Draw && Application.isEditor)
			Debug.DrawLine(startPosition, endPosition, Color.green);
	}
}
