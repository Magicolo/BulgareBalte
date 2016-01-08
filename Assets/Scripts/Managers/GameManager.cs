using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using Pseudo;
using System;
using Pseudo.Internal.Pool;

public class GameManager : Singleton<GameManager>
{
	public enum GameStates { Playing, Loading, Winning, Losing };

	public GameStates CurrentState { get; private set; }

	public string[] LevelNames;
	public float WinDelay = 3f;
	public float LoseDelay = 2.97f;
	public AudioSettingsBase WinSound;
	public AudioSettingsBase LoseSound;

	int currentSceneIndex = -1;
	AsyncOperation loadingTask;
	float counter;
	IEntityGroup spectatorGroup = EntityManager.GetEntityGroup(EntityGroups.Spectator);

	public string CurrentSceneName { get { return LevelNames[currentSceneIndex]; } }

	protected override void Awake()
	{
		base.Awake();

		PoolUtility.InitializeJanitor();
		currentSceneIndex = Array.IndexOf(LevelNames, SceneManager.GetActiveScene().name);
	}

	void Update()
	{
		switch (CurrentState)
		{
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

	public void StartGame()
	{
		LoadNextLevel();
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
			case GameStates.Playing:
				break;
			case GameStates.Loading:
				break;
			case GameStates.Winning:
				spectatorGroup.BroadcastMessage(EntityMessages.OnShowEvent, Spectator.ShowEventType.Win);
				counter = WinDelay;
				AudioManager.Instance.CreateItem(WinSound).Play();
				break;
			case GameStates.Losing:
				spectatorGroup.BroadcastMessage(EntityMessages.OnShowEvent, Spectator.ShowEventType.Lose);
				counter = LoseDelay;
				AudioManager.Instance.CreateItem(LoseSound).Play();
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