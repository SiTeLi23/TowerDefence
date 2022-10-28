using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    public Tower towerToPlace;

    public void SelectTower() 
    {
        if (!LevelManager.instance.levelActive) return;
        TowerManager.instance.StartTowerPlacement(towerToPlace);
    }
}
