using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Spectator")]
public class BroadcastToSpectatorOnMessage : ComponentBase, IMessageable
{
	[EnumFlags(typeof(EntityMessages))]
	public ByteFlag Messages;
	public Spectator.ShowEventType ShowEventType;

	IEntityGroup spectatorGroup = EntityManager.GetEntityGroup(EntityGroups.Spectator);

	public void OnMessage(EntityMessages message)
	{
		if (Messages[(byte)message])
			spectatorGroup.BroadcastMessage(EntityMessages.OnShowEvent, ShowEventType);
	}
}

