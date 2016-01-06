using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using Pseudo;
using System;

public class GameManager : Singleton<GameManager>
{

	public enum GameStates { None, Playing, Loading, Winning, Losing };

	public GameStates CurrentState { get; private set; }

	public string[] LevelNames;
	int currentSceneIndex = -1;

	public Scene[] Levels;

	public float WinDelay = 3f;
	public float LoseDelay = 2.97f;

	AsyncOperation loadingTask;
	float counter;

	public string CurrentSceneName { get { return LevelNames[currentSceneIndex]; } }

	protected override void Start()
	{
		base.Start();

		LoadNextLevel();
	}

	void Update()
	{
		switch (CurrentState)
		{
			case GameStates.None:
				break;
			case GameStates.Playing:
				break;
			case GameStates.Loading:
				if (loadingTask == null || loadingTask.isDone)
					SwitchState(GameStates.Playing);
				break;
			case GameStates.Winning:
				counter -= TimeManager.UI.DeltaTime;
				if (counter <= 0)
					LoadNextLevel();
				break;
			case GameStates.Losing:
				counter -= TimeManager.UI.DeltaTime;
				if (counter <= 0)
					ReloadCurrentLevel();
				break;
		}
	}

	public void LevelSuccess()
	{
		SwitchState(GameStates.Winning);
	}

	public void LevelFailure()
	{
		SwitchState(GameStates.Losing);
	}

	void SwitchState(GameStates state)
	{
		switch (CurrentState)
		{
			case GameStates.None:
				break;
			case GameStates.Playing:
				break;
			case GameStates.Loading:
				loadingTask = null;
				break;
			case GameStates.Winning:
				break;
			case GameStates.Losing:
				break;
		}

		CurrentState = state;

		switch (CurrentState)
		{
			case GameStates.None:
				break;
			case GameStates.Playing:
				break;
			case GameStates.Loading:
				break;
			case GameStates.Winning:
				counter = WinDelay;
				break;
			case GameStates.Losing:
				counter = LoseDelay;
				break;
		}
	}

	void LoadNextLevel()
	{
		SwitchState(GameStates.Loading);
		currentSceneIndex++;

		if (LevelNames.Length > currentSceneIndex)
			SwitchLevel(CurrentSceneName);
		else
			SwitchLevel("Credit");

	}

	void ReloadCurrentLevel()
	{
		SwitchState(GameStates.Loading);
		SwitchLevel(CurrentSceneName);
	}

	void SwitchLevel(string levelName)
	{
		loadingTask = SceneManager.LoadSceneAsync(levelName);
	}

}