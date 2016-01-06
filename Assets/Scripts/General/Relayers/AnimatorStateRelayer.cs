using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

public class AnimatorStateRelayer : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetEntity().SendMessage(EntityMessages.OnStateEnter, stateInfo);
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.GetEntity().SendMessage(EntityMessages.OnStateExit, stateInfo);
	}
}

