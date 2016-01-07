using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Camera")]
public class CameraComponent : ComponentBase, IUpdateable
{
	public float UpdateRate { get { return 0; } }

	public Camera Camera;
	[Space]
	public CameraBound CameraBound;
	public EntityMatch Fellow;

	public float lerpAmount = 3;

	public void Update()
	{
		var targets = EntityManager.GetEntityGroup(Fellow).Entities;

		if (targets.Count == 0) return;

		Vector3 targetsCenter = calculateTargetsCenter(targets);
		Vector3 targetInBound = putTargetInBound(targetsCenter, CameraBound.RectZone);
		//Vector3 smoothedPosition = smooth(targetInBound);

		//Entity.Transform.position = targetInBound;
		Entity.Transform.TranslateTowards(targetInBound, lerpAmount, Axes.XY);
	}

	private Vector3 calculateTargetsCenter(IList<IEntity> targets)
	{
		Vector3 positionCumulator = targets[0].Transform.position;
		for (int i = 1; i < targets.Count; i++)
			positionCumulator += targets[i].Transform.position;

		Vector3 targetsCenter = positionCumulator.Div(new Vector3(targets.Count, targets.Count, targets.Count));

		return targetsCenter;
	}

	private Vector3 putTargetInBound(Vector3 targetsCenter, RectZone rectZone)
	{
		Rect cameraRect = Camera.GetWorldRect();
		Rect bound = rectZone.WorldRect;
		float x = targetsCenter.x, y = targetsCenter.y;
		float halfCamWidth = cameraRect.width / 2;
		float halfCamHeight = cameraRect.height / 2;

		if (targetsCenter.x - halfCamWidth < bound.xMin)
			x = bound.xMin + halfCamWidth;
		else if (targetsCenter.x + halfCamWidth > bound.xMax)
			x = bound.xMax - halfCamWidth;

		if (targetsCenter.y - halfCamHeight < bound.yMin)
			y = bound.yMin + halfCamHeight;
		else if (targetsCenter.y + halfCamHeight > bound.yMax)
			y = bound.yMax - halfCamHeight;

		return new Vector3(x, y, targetsCenter.z);
	}

	private Vector3 smooth(Vector3 targetInBound)
	{
		throw new NotImplementedException();
	}
}

