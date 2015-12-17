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

	protected override void UpdateCast()
	{
		Hits.Clear();
		Vector3 position = CachedTransform.position;
		Vector3 direction = CachedTransform.rotation * Vector3.right;
		BounceCount = 0;
		Reflector reflector;
		bool startInCollider = Physics2D.queriesStartInColliders;

		do
		{
			reflector = null;
			var hit = Physics2D.Raycast(position, direction, Distance, Mask);
			Physics2D.queriesStartInColliders = false;

			if (hit.collider == null)
			{
				EndPosition = position + direction * Distance;
				DrawLine(position, EndPosition);
			}
			else
			{
				DrawLine(position, hit.point);
				var entity = hit.collider.GetEntity();
				reflector = entity == null ? null : entity.GetComponent<Reflector>();
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

		Physics2D.queriesStartInColliders = startInCollider;
		EndDirection = direction;
	}

	void DrawLine(Vector3 startPosition, Vector3 endPosition)
	{
		if (Draw && Application.isEditor)
			Debug.DrawLine(startPosition, endPosition, Color.green);
	}
}
