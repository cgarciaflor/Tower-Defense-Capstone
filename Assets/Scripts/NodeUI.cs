using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;
    public Button upgdBTN;

    public TMPro.TextMeshProUGUI upgdCost;
    public TMPro.TextMeshProUGUI sellAMNT;


    public void setTarget(Node target)
    {
        this.target = target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgdCost.text = "$" + target.turrtBlueprint.upgdCost;
            upgdBTN.interactable = true;
        }
        else {
            upgdCost.text = "MAX";
            upgdBTN.interactable = false;
        }

        sellAMNT.text = "$" + target.turrtBlueprint.GetSellPrice();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.Instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.Instance.DeselectNode();
    }
}
