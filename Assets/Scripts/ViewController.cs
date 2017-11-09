using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

    public float speed = 1;

    private Transform transform;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        //控制视野移动
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //控制视野缩放
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h, -mouse * speed, v), Space.World);
	}
}
