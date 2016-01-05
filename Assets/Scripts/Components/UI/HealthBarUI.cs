using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class HealthBarUI : ComponentBase
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

	IEntityGroup playerGroup = EntityManager.GetEntityGroup(EntityGroups.Player).Filter(typeof(Status));
	float currentAlpha;
	float targetAlpha;
	float currentHealthValue;
	float targetHealthValue;
	float currentDamageValue;
	float targetDamageValue;
	float delayCounter;

	readonly CachedValue<CanvasGroup> cachedCanvasGroup;
	public CanvasGroup CachedCanvasGroup { get { return cachedCanvasGroup.Value; } }

	public HealthBarUI()
	{
		cachedCanvasGroup = new CachedValue<CanvasGroup>(Entity.GameObject.GetComponent<CanvasGroup>);
	}

	void Update()
	{
		if (playerGroup.Entities.Count > PlayerIndex)
		{
			var status = playerGroup.Entities[PlayerIndex].GetComponent<Status>();

			if (targetHealthValue != (targetHealthValue = status.CurrentHealth / status.Health))
			{
				targetAlpha = ActiveAlpha;
				delayCounter = 0f;
			}

			if (delayCounter > ChangeDelay)
				targetDamageValue = targetHealthValue;

			if (delayCounter > FadeDelay)
				targetAlpha = InactiveAlpha;
		}
		else
			targetAlpha = 0f;

		delayCounter += TimeManager.UI.DeltaTime;
		currentHealthValue = Mathf.Lerp(currentHealthValue, targetHealthValue, ChangeSpeed * 5f * TimeManager.UI.DeltaTime);
		currentDamageValue = Mathf.Lerp(currentDamageValue, targetDamageValue, ChangeSpeed * TimeManager.UI.DeltaTime);
		currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, FadeSpeed * TimeManager.UI.DeltaTime);

		if (Mathf.Abs(HealthSlider.value - currentHealthValue) > 0.0001f)
			HealthSlider.value = currentHealthValue;

		if (Mathf.Abs(DamageSlider.value - currentDamageValue) > 0.0001f)
			DamageSlider.value = currentDamageValue;

		if (Mathf.Abs(CachedCanvasGroup.alpha - currentAlpha) > 0.0001f)
			CachedCanvasGroup.alpha = currentAlpha;
	}
}
