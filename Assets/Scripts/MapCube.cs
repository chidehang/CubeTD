using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {

    public GameObject builtEffect;
    public GameObject releaseEffect;

    [HideInInspector]
    public GameObject turret; //该地块上的炮塔
    [HideInInspector]
    public TurretData turretData; //该地块上的炮塔数据
    [HideInInspector]
    public bool isLevelTop = false; //炮塔已升至顶级

    private Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //建造炮塔
    public void BuildTurret(TurretData data)
    {
        isLevelTop = false;
        this.turret = GameObject.Instantiate(data.initialTurret, 
            new Vector3(transform.position.x, transform.position.y+ data.initialTurret.transform.position.y, transform.position.z), 
            Quaternion.identity);
        this.turretData = data;

        GameObject effect = GameObject.Instantiate(builtEffect,
            new Vector3(transform.position.x, transform.position.y + data.initialTurret.transform.position.y, transform.position.z),
            Quaternion.identity);
        Destroy(effect, 1);
    }

    //升级炮塔
    public bool UpgradedTurret()
    {
        if (isLevelTop) return false;
        Destroy(turret);
        isLevelTop = true;
        this.turret = GameObject.Instantiate(this.turretData.upgradedTurret,
            new Vector3(transform.position.x, transform.position.y + this.turretData.upgradedTurret.transform.position.y, transform.position.z),
            Quaternion.identity);

        GameObject effect = GameObject.Instantiate(builtEffect,
            new Vector3(transform.position.x, transform.position.y + this.turretData.upgradedTurret.transform.position.y, transform.position.z),
            Quaternion.identity);
        Destroy(effect, 1);
        return true;
    }


    //拆除炮塔
    public int ReleaseTurret()
    {
        GameObject effect = GameObject.Instantiate(releaseEffect,
            new Vector3(transform.position.x, transform.position.y + this.turret.transform.position.y, transform.position.z),
            Quaternion.identity);
        Destroy(turret);
        Destroy(effect, 1);

        int money = isLevelTop ? (turretData.upgradedCost+turretData.cost) : turretData.cost;

        isLevelTop = false;
        turret = null;
        turretData = null;

        return money;
    }

    private void OnMouseEnter()
    {
        if(turret==null && !EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
}
