using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgdPrefab;
    public int upgdCost;

    public int GetSellPrice()
    {
        return cost / 2;
    }
}
