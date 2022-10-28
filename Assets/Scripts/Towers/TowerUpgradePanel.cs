using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerUpgradePanel : MonoBehaviour
{
    public GameObject rangeButton, fireRateButton;

    public TMP_Text rangeText, fireRateText;

    public void SetupPanel() 
    {

        if (TowerManager.instance.selectedTower.upgrader.hasRangedUpgrade) 
        { 

           TowerUpgradeController upgrader = TowerManager.instance.selectedTower.upgrader;
           rangeText.text = "Upgrade\nRange\n (" + upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost + "G)";

           rangeButton.SetActive(true);
        }
        else 
        {
            rangeButton.SetActive(false);
        }

        if (TowerManager.instance.selectedTower.upgrader.hasFireRateUpgrade)
        {
            TowerUpgradeController upgrader = TowerManager.instance.selectedTower.upgrader;
            fireRateText.text = upgrader.fireRateText + "(" + upgrader.fireRateUpgrades[upgrader.currentFireRateUpgrade].cost + "G)";
            fireRateButton.SetActive(true);
        }
        else
        {
            fireRateButton.SetActive(false);
        }
    }

    #region Button Functions
    public void RemoveTower() 
    {
        MoneyManager.instance.SpendMoney(-50);
        Destroy(TowerManager.instance.selectedTower.gameObject);
        UIController.instance.CloseTowerUpGradePanel();

        AudioManager.instance.PlaySFX(9);
    }

    public void UpgradeRange() 
    {
        TowerUpgradeController upgrader = TowerManager.instance.selectedTower.upgrader;

        if (upgrader.hasRangedUpgrade) 
        {
            if (MoneyManager.instance.SpendMoney(upgrader.rangeUpgrades[upgrader.currentRangeUpgrade].cost)) 
            {
                upgrader.UpgradeRange();

                SetupPanel();

                UIController.instance.notEnoughMoneyWarning.SetActive(false);

                AudioManager.instance.PlaySFX(10);
            }
            else 
            {
                UIController.instance.notEnoughMoneyWarning.SetActive(true);
            }
        }
    }

    public void UpgradeFireRate()
    {
        TowerUpgradeController upgrader = TowerManager.instance.selectedTower.upgrader;

        if (upgrader.hasFireRateUpgrade)
        {
            if (MoneyManager.instance.SpendMoney(upgrader.fireRateUpgrades[upgrader.currentFireRateUpgrade].cost))
            {
                upgrader.UpgradeFireRange();

                SetupPanel();

                UIController.instance.notEnoughMoneyWarning.SetActive(false);

                AudioManager.instance.PlaySFX(10);
            }
            else 
            {
                UIController.instance.notEnoughMoneyWarning.SetActive(true);
            }
        }
    }
    #endregion
}
