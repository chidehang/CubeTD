using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData {
    public GameObject initialTurret;
    public int cost;
    public GameObject upgradedTurret;
    public int upgradedCost;
    public TurretType type;
}

public enum TurretType
{
    LaserTurret,
    MissleTurret,
    StandarsTurret
}
