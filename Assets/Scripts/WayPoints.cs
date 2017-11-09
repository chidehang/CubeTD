using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {

    public static Transform[] wayPositions;

    private void Awake()
    {
        wayPositions = new Transform[transform.childCount];
        for(int i=0; i<wayPositions.Length; i++)
        {
            wayPositions[i] = transform.GetChild(i);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
