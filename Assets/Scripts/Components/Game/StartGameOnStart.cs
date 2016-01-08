using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[System.Serializable, ComponentCategory("Game")]
public class StartGameOnStart : IMessageable
{
	public void OnMessage(EntityMessages message)
	{
		GameManager.Instance.StartGame();
	}

	public void Start()
	{

	}
}

