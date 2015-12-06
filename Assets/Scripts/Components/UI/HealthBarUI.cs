using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class HealthBarUI : PComponent
{
	public Slider HealthSlider;
	public Slider DamageSlider;
	public float ActiveAlpha = 1f;
	public float InactiveAlpha = 0.5f;
	public float FadeSpeed = 2.5f;
	public float FadeDelay = 3f;
	public float ChangeSpeed = 5f;
	public float ChangeDelay = 1f;
	public int PlayerIndex;

	IEntityGroup playerGroup;
	float targetAlpha;
	float healthValue;
	float damageValue;
	float delayCounter;

	readonly CachedValue<CanvasGroup> cachedCanvasGroup;
	public CanvasGroup CachedCanvasGroup { get { return cachedCanvasGroup.Value; } }

	public HealthBarUI()
	{
		cachedCanvasGroup = new CachedValue<CanvasGroup>(GetComponent<CanvasGroup>);
	}

	void Awake()
	{
		playerGroup = EntityManager.GetEntityGroup(EntityGroups.Player).Filter(typeof(Status));
		CachedCanvasGroup.alpha = 0f;
	}

	void Update()
	{
		if (playerGroup.Entities.Count > PlayerIndex)
		{
			var status = playerGroup.Entities[PlayerIndex].GetComponent<Status>();

			if (healthValue != (healthValue = status.CurrentHealth / status.Health))
			{
				targetAlpha = ActiveAlpha;
				delayCounter = 0f;
			}

			if (delayCounter > ChangeDelay)
				damageValue = healthValue;

			if (delayCounter > FadeDelay)
				targetAlpha = InactiveAlpha;
		}
		else
			targetAlpha = 0f;

		delayCounter += TimeManager.UI.DeltaTime;
		HealthSlider.value = Mathf.Lerp(HealthSlider.value, healthValue, ChangeSpeed * 5f * TimeManager.UI.DeltaTime);
		DamageSlider.value = Mathf.Lerp(DamageSlider.value, damageValue, ChangeSpeed * TimeManager.UI.DeltaTime);
		CachedCanvasGroup.alpha = Mathf.Lerp(CachedCanvasGroup.alpha, targetAlpha, FadeSpeed * TimeManager.UI.DeltaTime);
	}
}
