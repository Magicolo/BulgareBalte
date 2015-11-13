using UnityEngine;
using System.Collections;
using Pseudo;

public class FrequencySpawner : MonoBehaviour {

    public Enemy EnemyToSpawn;
    public MinMax spawningFrequency;

    [Disable]public float nextSpawn;
	
	void Start () {
        nextSpawn = spawningFrequency.GetRandom(ProbabilityDistributions.Normal);
    }
	
	void Update () {
        nextSpawn -= TimeManager.Enemy.DeltaTime;
        if (nextSpawn <= 0) 
        {

        }
	}
}
