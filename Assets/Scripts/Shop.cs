using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLaunch;
    public TurretBlueprint laserBeam;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectStandardTurret()
    {
        Debug.Log("Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Launcher Purchased");
        buildManager.SelectTurretToBuild(missileLaunch);
    }

    public void SelectLaserBeam() {
        Debug.Log("laser beam selected");
        buildManager.SelectTurretToBuild(laserBeam);
    }
}
