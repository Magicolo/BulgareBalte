using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Spectator")]
public class OnDieBroadcastToSpectator : ComponentBase
{
	public Spectator.ShowEventType ShowEventType;

	public void OnDie()
	{
		EntityManager.GetEntityGroup(EntityGroups.Spectator).BroadcastMessage(EntityMessages.OnShowEvent, ShowEventType);
	}
}

