using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Spectator")]
public class OnDieBroadcastToSpectator : ComponentBase
{
	public Spectator.ShowEventType ShowEventType;

	IEntityGroup SpectatorGroup = EntityManager.GetEntityGroup(EntityGroups.Spectator);

	public void OnDie()
	{
		SpectatorGroup.BroadcastMessage(EntityMessages.OnShowEvent, ShowEventType);
	}
}

