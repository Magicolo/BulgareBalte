﻿using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable]
public class RandomSpriteOnStart : ComponentBase, IStartable
{
	public SpriteRenderer SpriteRenderer;
	public Sprite[] Sprites;

	public void Start()
	{
		SpriteRenderer.sprite = Sprites.GetRandom();
		Entity.RemoveComponent(this);
	}
}
