using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one in scene");
            return;
        }
        Instance = this;

    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }


    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return ;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.setTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret; 
        DeselectNode();

    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
