using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("Motion"), EntityRequires(typeof(TimeComponent))]
public class RotateOnMessage : ComponentBase, IStartable, IUpdateable, IMessageable
{
	[EnumFlags(typeof(EntityMessages))]
	public ByteFlag Messages;
	public float Increment = 90f;
	public float Speed = 5f;
	public Transform ToRotate;

	float targetAngle;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public void Start()
	{
		targetAngle = ToRotate.localEulerAngles.z;
	}

	public void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();
		ToRotate.RotateLocalTowards(targetAngle, time.DeltaTime * Speed, Axes.Z);
	}

	public void OnMessage(EntityMessages message)
	{
		if (Messages[(byte)message])
			targetAngle += Increment;
	}
}
