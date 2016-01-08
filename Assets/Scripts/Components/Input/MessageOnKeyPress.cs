using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Input")]
public class MessageOnKeyPress : ComponentBase, IUpdateable
{
	public KeyCode KeyPressed;

	public EntityMessages Message;

	public float UpdateRate { get { return 0; } }

	public void Update()
	{
		if (Input.GetKeyDown(KeyPressed))
			Entity.SendMessage(Message);
	}
}

