using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[ExecuteInEditMode, RequireComponent(typeof(MeshRenderer))]
public class TextureTiler : PMonoBehaviour
{
	readonly CachedValue<MeshRenderer> cachedRenderer;
	public MeshRenderer CachedRenderer { get { return cachedRenderer; } }

	public TextureTiler()
	{
		cachedRenderer = new CachedValue<MeshRenderer>(GetComponent<MeshRenderer>);
	}

	void Update()
	{
		CachedRenderer.sharedMaterial.mainTextureScale = CachedTransform.localScale;
	}
}
