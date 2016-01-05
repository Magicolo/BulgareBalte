using UnityEngine;
using System.Collections.Generic;

using Pseudo;

public class GameManager : Singleton<GameManager>
{

	public enum GameStates { None, Playing, Loading, Winning, Losing };
	[Disable]
	public GameStates CurrentGameState;

	public string[] LevelNames;

	protected override void Awake()
	{
		base.Awake();
	}

	void Update()
	{

	}
}
