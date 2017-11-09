using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour {

    public float attackInterval = 0.3f;
    public GameObject bullet;
    public Transform muzzleTransform;
    public Transform head;
    public bool useLayser; //使用激光武器
    public GameObject layserEffect;

    private float attackTimer;

    private List<GameObject> enemys = new List<GameObject>();

    private LineRenderer layserRender;

	// Use this for initialization
	void Start () {
        attackTimer = attackInterval;
        layserRender = GetComponentInChildren<LineRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        if(enemys.Count > 0)
            RotateHead();

        if (enemys.Count>0 && enemys[0] == null) RemoveEnemy();

        if (useLayser && layserRender)
        {
            if(enemys.Count > 0)
            {
                layserRender.enabled = true;

                Transform enemy = enemys[0].transform;
                layserRender.SetPositions(new Vector3[] { muzzleTransform.position, enemy.position });
                enemys[0].GetComponent<EnemyController>().TakeDamaged(Time.deltaTime * (1/attackInterval) * 8);

                layserEffect.SetActive(true);
                layserEffect.transform.position = new Vector3(enemy.position.x, enemy.position.y, enemy.position.z + 1);
                layserEffect.transform.LookAt(muzzleTransform);
            }
            else
            {
                layserEffect.SetActive(false);
                layserRender.enabled = false;
            }
        }
        else if(bullet)
        {
            if (enemys.Count > 0)
            {
                if (attackTimer >= attackInterval)
                {
                    Attack();
                    attackTimer = 0;
                }
                attackTimer += Time.deltaTime;
            }
            else
            {
                attackTimer = attackInterval;
            }
        }
	}

    public void RotateHead()
    {
        if (enemys[0] == null) RemoveEnemy();

        if (enemys.Count > 0)
        {
            Transform enemy = enemys[0].transform;
            head.LookAt(enemy.transform);
        }
    }


    public void Attack()
    {
        if (enemys[0] == null) RemoveEnemy();

        if (enemys.Count > 0)
        {
            Transform enemy = enemys[0].transform;
            GameObject gameObject = GameObject.Instantiate(bullet, muzzleTransform.position, muzzleTransform.rotation);
            gameObject.GetComponent<Bullet>().setTarget(enemy);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Enemy")) {
            enemys.Remove(other.gameObject);
        }
    }

    public void RemoveEnemy()
    {
        List<GameObject> newEnemys = new List<GameObject>();
        foreach(GameObject item in enemys)
        {
            if (item != null)
                newEnemys.Add(item);
        }
        enemys = newEnemys;
    }
}
