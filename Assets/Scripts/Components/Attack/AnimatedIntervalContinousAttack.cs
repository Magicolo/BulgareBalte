using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable]
public class AnimatedIntervalContinousAttack : WeaponAttack, IUpdateable, IStartable
{
	public MinMax ContinuousDuration = new MinMax(0.75f, 1.5f);

	float nextAttackTime;
	float nextStopTime;

	public Animator Animator;
	public string ChargeAnimationName;
	public string ChargeTriggerName;

	public string StopAttackingTriggerName;

	public enum State { Idle, Charging, Attack, Reloading }
	[Disable]
	public State currentState;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public override void Start()
	{
		base.Start();
		startCharging();
	}

	public virtual void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();
		switch (currentState)
		{
			case State.Idle:
				break;
			case State.Charging:
				break;
			case State.Attack:
				Attack();
				if (time.Time > nextStopTime)
					StopAttacking(time);
				break;
			case State.Reloading:
				if (time.Time > nextAttackTime)
					startCharging();
				break;
		}


	}

	private void startCharging()
	{
		currentState = State.Charging;
		Animator.SetTrigger(ChargeTriggerName);
	}

	private void startAttacking()
	{
		nextStopTime = nextAttackTime + ContinuousDuration.GetRandom();
		currentState = State.Attack;
		Entity.SendMessage(EntityMessages.OnStartAttacking);
	}

	private void StopAttacking(TimeComponent time)
	{
		nextAttackTime = time.Time + 1f / AttackSpeed;
		Animator.SetTrigger(StopAttackingTriggerName);
		currentState = State.Reloading;
		Entity.SendMessage(EntityMessages.OnStopAttacking);
	}

	void OnStateExit(AnimatorStateInfo info)
	{
		if (info.IsName(ChargeAnimationName))
			startAttacking();
	}
}
