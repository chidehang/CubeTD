using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public static int AliveEnemyCount = 0;

    public Wave[] waves;
    public Transform startPos;

    private Coroutine spwanCoroutine;

	// Use this for initialization
	void Start () {
        spwanCoroutine = StartCoroutine(SpawnEnemy());
    }

    public void StopSpwan()
    {
        StopCoroutine(spwanCoroutine);
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for(int i=0; i<wave.count; i++)
            {
                GameObject.Instantiate(wave.enemy, startPos.position, Quaternion.identity);
                AliveEnemyCount++;
                yield return new WaitForSeconds(wave.rate);
            }
            while (AliveEnemyCount > 0)
                yield return 0;
            yield return new WaitForSeconds(2);
        }

        while (AliveEnemyCount > 0) yield return 0;
        GameManager.Instance.Win();
    }
}
