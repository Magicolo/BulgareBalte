using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[System.Serializable, ComponentCategory("Game")]
public class StartGameOnMessage : ComponentBase, IMessageable
{
	[EnumFlags(typeof(EntityMessages))]
	public ByteFlag Message;

	public void OnMessage(EntityMessages message)
	{
		GameManager.Instance.StartGame();
	}
}

