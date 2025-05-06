using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor; 
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;

    [HideInInspector]
    public TurretBlueprint turrtBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    public AudioClip sellSFX;
    public AudioClip upgradeSFX;
    public AudioClip buildSFX;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.Instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void SellTurret()
    {
        PlayerStats.Money += turrtBlueprint.GetSellPrice();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2.5f);

        Destroy(turret);
        turrtBlueprint = null;
        if (sellSFX != null)
        {
            SoundsManager.instance.PlaySoundFX(sellSFX, transform);
        }
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }       

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) { return; }

        // build turret
        BuildTurret(buildManager.GetTurretToBuild());

    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turrtBlueprint = blueprint;


        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2.5f);
        if (buildSFX != null)
        {
            SoundsManager.instance.PlaySoundFX(buildSFX, transform);
        }
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turrtBlueprint.upgdCost)
        {
            Debug.Log("Not enough money to uypgrade");
            return;
        }

        PlayerStats.Money -= turrtBlueprint.upgdCost;

        // Get rid of un-upgraded turret
        Destroy(turret);

        // build the new upgraded turret
        GameObject _turret = Instantiate(turrtBlueprint.upgdPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2.5f);
        
        isUpgraded = true;
        if (upgradeSFX != null)
        {
            SoundsManager.instance.PlaySoundFX(upgradeSFX, transform);
        }
        Debug.Log("upgrade complete");
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild) return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        } else {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
