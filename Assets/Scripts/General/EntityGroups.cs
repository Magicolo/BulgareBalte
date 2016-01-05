using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class EntityGroups : EntityGroupDefinition
{
	public static readonly EntityGroups Player = new EntityGroups(0);
	public static readonly EntityGroups Enemy = new EntityGroups(1);

	public EntityGroups(params byte[] groupIds) : base(groupIds) { }
}