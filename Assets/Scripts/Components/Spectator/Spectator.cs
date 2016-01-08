using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Spectator"), RequireComponent(typeof(TimeComponent))]
public class Spectator : ComponentBase, IUpdateable, IStartable
{
	TimeComponent Time;
	Animator Animator;

	public MinMax ChanceToClap;
	public MinMax ChanceToBanana;
	public MinMax ChanceToCough;
	public MinMax ChanceToBoo;
	public MinMax ChanceToFirst1;
	public MinMax ChanceToFirst2;
	public MinMax ChanceToWave;

	public AudioSettingsBase ClapSound;
	public AudioSettingsBase AngrySound;
	public AudioSettingsBase CheerSound;
	public AudioSettingsBase CoughSound;
	public AudioSettingsBase BooSound;

	float likesToClap;
	float likesToBanana;
	float likesToCough;
	float likesToBoo;
	float likesToFirst1;
	float likesToFirst2;
	float likesToWave;

	public enum ShowEventType { PlayerDie, SmallEnemisDie, AverageEnemisDie, BigEnemisDie, BigEnemisSpawns }

	float nextIdleTime;
	float idleTimeBetween = 1;

	public float UpdateRate { get { return 0.25f; } }

	public void Update()
	{
		if (Time.Time > nextIdleTime)
		{
			updateIdleTime();
			float random = PRandom.Range(0f, 1f);
			Animator.SetTrigger("ReturnToNormal");

			if (random < likesToBanana)
				Animator.SetTrigger("IdleBanana");
			else if (random < likesToBanana + likesToCough)
			{
				Animator.SetTrigger("IdleCough");
				AudioManager.Instance.CreateItem(CoughSound, Entity.Transform.position).Play();
			}
		}
	}

	public void Start()
	{
		Time = Entity.GetComponent<TimeComponent>();
		Animator = Entity.GameObject.GetComponent<Animator>();
		updateIdleTime();

		likesToClap = ChanceToClap.GetRandom(ProbabilityDistributions.Uniform);
		likesToBanana = ChanceToBanana.GetRandom(ProbabilityDistributions.Uniform);
		likesToCough = ChanceToCough.GetRandom(ProbabilityDistributions.Uniform);
		likesToBoo = ChanceToBoo.GetRandom(ProbabilityDistributions.Uniform);
		likesToFirst1 = ChanceToFirst1.GetRandom(ProbabilityDistributions.Uniform);
		likesToFirst2 = ChanceToFirst2.GetRandom(ProbabilityDistributions.Uniform);
		likesToWave = ChanceToWave.GetRandom(ProbabilityDistributions.Uniform);
	}

	void updateIdleTime()
	{
		nextIdleTime = Time.Time + idleTimeBetween + PRandom.Range(0f, 1f);
	}

	public void OnShowEvent(ShowEventType showEventType)
	{
		switch (showEventType)
		{
			case ShowEventType.PlayerDie:
				UnHappyAnimation(1f);
				break;
			case ShowEventType.SmallEnemisDie:
				HappyAnimation(50);
				break;
			case ShowEventType.AverageEnemisDie:
				HappyAnimation(7);
				break;
			case ShowEventType.BigEnemisDie:
				HappyAnimation(1f);
				break;
			case ShowEventType.BigEnemisSpawns:
				HappyAnimation(1f);
				break;
			default:
				break;
		}
	}

	void HappyAnimation(float factor)
	{
		float random = PRandom.Range(0f, 1f) * factor;

		if (random < likesToWave)
		{
			animate("Wave");
			AudioManager.Instance.CreateItem(CheerSound, Entity.Transform.position).Play();
		}
		else if (random < likesToWave + likesToFirst1)
		{
			animate("First1");
			AudioManager.Instance.CreateItem(CheerSound, Entity.Transform.position).Play();
		}
		else if (random < likesToWave + likesToFirst1 + likesToClap)
		{
			animate("Clap");
			AudioManager.Instance.CreateItem(ClapSound, Entity.Transform.position).Play();
		}
	}

	void UnHappyAnimation(float factor)
	{
		float random = PRandom.Range(0f, 1f) * factor;

		if (random < likesToBoo)
		{
			animate("Booouu");
			AudioManager.Instance.CreateItem(BooSound, Entity.Transform.position).Play();
		}
		else if (random < likesToBoo + likesToFirst2)
		{
			animate("First2");
			AudioManager.Instance.CreateItem(AngrySound, Entity.Transform.position).Play();
		}
	}

	private void animate(string trigger)
	{
		Animator.SetTrigger(trigger);
		updateIdleTime();
	}
}

