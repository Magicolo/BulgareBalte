using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Animator")]
public class AnimateOnStart : ComponentBase, IStartable
{

	public Animator Animator;
	public string TriggerName;

	public void Start()
	{
		Animator.SetTrigger(TriggerName);
	}
}

