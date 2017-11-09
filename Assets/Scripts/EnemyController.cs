using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public float speed = 1;
    public float hp = 100;
    public GameObject dieEffect;

    private Transform transform;
    private Slider hpSlider;

    private Transform[] wayPositions;
    private int index = 0;
    private float initialHP;

    private BuildManager buildManager;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
        wayPositions = WayPoints.wayPositions;
        hpSlider = GetComponentInChildren<Slider>();
        initialHP = hp;

        buildManager = GameManager.Instance.GetComponent<BuildManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public void Move()
    {
        if (index >= wayPositions.Length) {
            ReachDestination();
            return;
        }

        transform.Translate((wayPositions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(wayPositions[index].position, transform.position) <= 0.2f)
            index++;
    }

    public void ReachDestination()
    {
        GameObject.Destroy(this.gameObject);
        GameManager.Instance.Failed();

    }

    private void OnDestroy()
    {
        EnemySpawn.AliveEnemyCount--;
    }

    public void TakeDamaged(float damage)
    {
        if (hp <= 0) return;

        hp -= damage;
        hpSlider.value = (float)hp / initialHP;

        if(hp <= 0)
        {
            GameObject effect = GameObject.Instantiate(dieEffect, transform.position, transform.rotation);
            effect.GetComponent<ParticleSystem>().Play();
            Destroy(effect, 1);
            Destroy(this.gameObject);

            buildManager.ChangeMoney(10);
        }
    }
}
