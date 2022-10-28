using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    private Tower theTower;

    public UpgradeStage[] rangeUpgrades;
    public int currentRangeUpgrade;
    public bool hasRangedUpgrade = true;

    public UpgradeStage[] fireRateUpgrades;
    public int currentFireRateUpgrade;
    public bool hasFireRateUpgrade = true;
    [TextArea]
    public string fireRateText;
    void Start()
    {
        theTower = GetComponent<Tower>();
    }

    public void UpgradeRange() 
    {
        theTower.range = rangeUpgrades[currentRangeUpgrade].amount;
        currentRangeUpgrade++;
        if (currentRangeUpgrade >= rangeUpgrades.Length) 
        {
            hasRangedUpgrade = false;
        }
    }

    public void UpgradeFireRange()
    {
        theTower.fireRate = fireRateUpgrades[currentFireRateUpgrade].amount;
        currentFireRateUpgrade++;
        if (currentFireRateUpgrade >= fireRateUpgrades.Length)
        {
            hasFireRateUpgrade = false;
        }
    }
}

#region a public class to store upgrade information
[System.Serializable]
public class UpgradeStage 
{
    public float amount;
    public int cost;
}

#endregion
