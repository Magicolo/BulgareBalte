using UnityEngine;
using System.Collections.Generic;
using Pseudo;

[System.Serializable, ComponentCategory("Game")]
public class StartGameOnStart : ComponentBase, IStartable
{
	public void Start()
	{
		GameManager.Instance.StartGame();
	}
}

