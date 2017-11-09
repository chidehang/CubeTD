using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public TurretData standarsTurret;
    public TurretData missleTurret;
    public TurretData layserTurret;
    public Text moneyText;
    public int money = 300;

    private TurretData selectedTurret;  //选中的待建造炮塔类型数据
    private MapCube readyUpgradeMapCube; //准备升级炮塔的地块
    private Animator moneyAnim;

    private GameObject upgradeUI;
    private Button upgradeBtn;
    private Button releaseBtn; 

    private void Awake()
    {
        moneyAnim = moneyText.GetComponent<Animator>();
        upgradeUI = GameObject.Find("UpgradeUI");
        upgradeBtn = upgradeUI.GetComponentInChildren<Button>();
        releaseBtn = upgradeUI.GetComponentInChildren<Button>();
        HideUpgradeUI();
    }

    private void Start()
    {
        ChangeMoney();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                //鼠标坐标不在UI上
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Map")))
                {
                    //获取鼠标点击到的方块
                    GameObject cube = hit.collider.gameObject;
                    MapCube mapCube = cube.GetComponent<MapCube>();
                    if(mapCube.turret == null)
                    {
                        //建造炮塔
                        if(selectedTurret != null)
                        {
                            if (money >= selectedTurret.cost)
                            {
                                ChangeMoney(-selectedTurret.cost);
                                mapCube.BuildTurret(selectedTurret);
                            }
                            else
                            {
                                //金额不足
                                moneyAnim.SetTrigger("Flicker");
                            }
                        }
                    } else
                    {
                        //升级炮塔
                        if (readyUpgradeMapCube != mapCube)
                        {
                            //未选中该炮塔
                            ShowUpgradeUI(new Vector3(mapCube.transform.position.x, mapCube.transform.position.y+6, mapCube.transform.position.z+4), 
                                (mapCube.isLevelTop || money<mapCube.turretData.upgradedCost));
                            readyUpgradeMapCube = mapCube;
                        }
                        else
                        {
                            HideUpgradeUI();
                        }
                    }
                }
            }
        }

        if(readyUpgradeMapCube != null)
            upgradeBtn.interactable = money >= readyUpgradeMapCube.turretData.upgradedCost && !readyUpgradeMapCube.isLevelTop;
    }

    public void ChangeMoney(int changeMoney = 0)
    {
        money += changeMoney;
        moneyText.text = "¥" + money;
    }

    public void OnStandarsSelected(bool isOn)
    {
        if(isOn)
        {
            selectedTurret = standarsTurret;
        }
    }

    public void OnMissleSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = missleTurret;
        }
    }

    public void OnLayserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurret = layserTurret;
        }
    }

    public void ShowUpgradeUI(Vector3 anchorPos, bool isDisableUpgrade = false)
    {
        upgradeUI.SetActive(true);
        upgradeUI.transform.position = anchorPos;
        upgradeBtn.interactable = !isDisableUpgrade;
    }

    public void HideUpgradeUI()
    {
        upgradeUI.SetActive(false);
        readyUpgradeMapCube = null;
    }

    public void OnUpgradeBtnDown()
    {
        if (readyUpgradeMapCube.UpgradedTurret())
            ChangeMoney(-readyUpgradeMapCube.turretData.upgradedCost);
        HideUpgradeUI();
    }

    public void OnReleaseBtnDown()
    {
        int cost = readyUpgradeMapCube.ReleaseTurret();
        ChangeMoney(cost);
        HideUpgradeUI();
    }
}
