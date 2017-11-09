using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage = 10;
    public float speed = 50;

    private ParticleSystem hitEffect;
    private Transform target;

	// Use this for initialization
	void Start () {
        hitEffect = GetComponentInChildren<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        transform.LookAt(target);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    public void setTarget(Transform _target)
    {
        this.target = _target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamaged(damage);
            if(hitEffect)
            {
                hitEffect.Play();
            }
            Destroy(this.gameObject, 0.5f);
        }
    }
}
