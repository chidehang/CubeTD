using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//保存每一波敌人属性
[System.Serializable]
public class Wave {
    public GameObject enemy;
    public int count;
    public float rate;
}
