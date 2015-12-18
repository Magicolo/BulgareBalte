using UnityEngine;
using System.Collections.Generic;
using Pseudo;

public class RandomSpriteOnStart : PComponent
{

	public SpriteRenderer SpriteRenderer;
	public Sprite[] Sprites;

	protected override void Start()
	{
		SpriteRenderer.sprite = Sprites.GetRandom();
		this.Destroy();
	}

}
