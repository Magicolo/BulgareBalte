using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("Audio"), EntityRequires(typeof(TimeComponent))]
public class AudioActionOnMessage : ComponentBase, IStartable, IMessageable
{
	[Serializable]
	public class AudioAction
	{
		public enum AudioActions
		{
			Play,
			Pause,
			Resume,
			Stop,
			StopImmediate,
			StopAll,
			StopAllImmediate
		}

		public enum SpatializationModes
		{
			None,
			Static,
			Dynamic
		}

		[EnumFlags(typeof(EntityMessages))]
		public ByteFlag Messages;
		public AudioActions Action;
		public SpatializationModes Spatialization = SpatializationModes.Static;
		public bool ExecuteOnce;
		public float ExecutionDelay = 0.1f;
		public AudioSettingsBase Settings;

		public float LastExecutionTime { get; set; }
	}

	[InitializeContent]
	public AudioAction[] Actions;

	readonly List<AudioAction> activeActions = new List<AudioAction>();
	readonly Dictionary<int, List<AudioItem>> idActiveitems = new Dictionary<int, List<AudioItem>>();
	readonly List<AudioItem> activeItems = new List<AudioItem>();

	public void Start()
	{
		activeActions.AddRange(Actions);
	}

	void ExecuteAction(AudioAction action)
	{
		switch (action.Action)
		{
			case AudioAction.AudioActions.Play:
				Play(action);
				break;
			case AudioAction.AudioActions.Pause:
				Pause(action);
				break;
			case AudioAction.AudioActions.Resume:
				Resume(action);
				break;
			case AudioAction.AudioActions.Stop:
				Stop(action);
				break;
			case AudioAction.AudioActions.StopImmediate:
				StopImmediate(action);
				break;
			case AudioAction.AudioActions.StopAll:
				StopAll();
				break;
			case AudioAction.AudioActions.StopAllImmediate:
				StopAllImmediate();
				break;
		}
	}

	void Play(AudioAction action)
	{
		var items = GetItems(action.Settings.Id);
		AudioItem item = null;

		switch (action.Spatialization)
		{
			case AudioAction.SpatializationModes.None:
				item = AudioManager.Instance.CreateItem(action.Settings);
				break;
			case AudioAction.SpatializationModes.Static:
				item = AudioManager.Instance.CreateItem(action.Settings, Entity.Transform.position);
				break;
			case AudioAction.SpatializationModes.Dynamic:
				item = AudioManager.Instance.CreateItem(action.Settings, Entity.Transform);
				break;
		}

		activeItems.Add(item);
		items.Add(item);
		item.OnStop += i => { activeItems.Remove(i); items.Remove(i); };
		item.Play();
	}

	void Pause(AudioAction action)
	{
		var items = GetItems(action.Settings.Id);

		for (int i = 0; i < items.Count; i++)
			items[i].Pause();
	}

	void Resume(AudioAction action)
	{
		var items = GetItems(action.Settings.Id);

		for (int i = 0; i < items.Count; i++)
			items[i].Resume();
	}

	void Stop(AudioAction action)
	{
		var items = GetItems(action.Settings.Id);

		for (int i = 0; i < items.Count; i++)
			items[i].Stop();
	}

	void StopImmediate(AudioAction action)
	{
		var items = GetItems(action.Settings.Id);

		for (int i = 0; i < items.Count; i++)
			items[i].StopImmediate();
	}

	void StopAll()
	{
		for (int i = activeItems.Count; i-- > 0;)
			activeItems[i].Stop();
	}

	void StopAllImmediate()
	{
		for (int i = activeItems.Count; i-- > 0;)
			activeItems[i].StopImmediate();
	}

	List<AudioItem> GetItems(int id)
	{
		List<AudioItem> items;

		if (!idActiveitems.TryGetValue(id, out items))
		{
			items = new List<AudioItem>();
			idActiveitems[id] = items;
		}

		return items;
	}

	public void OnMessage(EntityMessages message)
	{
		var time = Entity.GetComponent<TimeComponent>();

		for (int i = 0; i < activeActions.Count; i++)
		{
			var action = activeActions[i];

			if (action.LastExecutionTime + action.ExecutionDelay < time.Time && action.Messages[(byte)message])
			{
				ExecuteAction(action);

				action.LastExecutionTime = time.Time;

				if (action.ExecuteOnce)
					activeActions.RemoveAt(i--);
			}
		}
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		activeItems.Clear();
		activeActions.Clear();
		idActiveitems.Clear();
	}
}
