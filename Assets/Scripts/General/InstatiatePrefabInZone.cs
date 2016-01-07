using UnityEngine;
using System.Collections;
using Pseudo;

public class InstatiatePrefabInZone : MonoBehaviour
{
	public float Chance;
	public GameObject Prefab;
	public Zone2DBase Zone;
	public Transform ParentTransform;

	public float angle;

	void Start()
	{
		if (Random.Range(0f, 1f) <= Chance)
		{
			var newGO = Instantiate(Prefab);
			newGO.transform.parent = this.transform;
			newGO.transform.localPosition = Zone.GetRandomLocalPoint();
			if (ParentTransform != null)
				newGO.transform.parent = ParentTransform;
			newGO.transform.Rotate(angle, Axes.Z);
		}

		this.Destroy();
	}
}
