using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[EntityGroups]
public static class EntityGroups
{
	public static readonly ByteFlag Player = new ByteFlag(0);
	public static readonly ByteFlag Enemy = new ByteFlag(1);
	public static readonly ByteFlag Spectator = new ByteFlag(2);
}