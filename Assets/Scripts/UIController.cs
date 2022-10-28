using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        #region Singleton

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        #endregion
    }

    public TMP_Text goldText;
    public GameObject notEnoughMoneyWarning;

    public GameObject levelCompleteScreen, levelFailedScreen;

    public GameObject towerButtons;

    public GameObject pauseScreen;

    public string levelSelctScene, mainMenuScene;

    public TMP_Text waveText;

    //tower upgrade
    public TowerUpgradePanel towerUpgradePanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseUnPause();
        }
    }

    public void PauseUnPause() 
    {
        if (!pauseScreen.activeSelf) 
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else 
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void LevelSelect() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSelctScene);
    }

    public void MainMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void TryAgain() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel() 
    {
        if (LevelManager.instance.nextLevel != null)
        {
            SceneManager.LoadScene(LevelManager.instance.nextLevel);
        }
    }

    public void OpenTowerUpGradePanel() 
    {
        if (LevelManager.instance.levelActive) 
        {
           towerUpgradePanel.gameObject.SetActive(true);
           towerUpgradePanel.SetupPanel();
        }

    }

    public void CloseTowerUpGradePanel()
    {
        towerUpgradePanel.gameObject.SetActive(false);

        if (TowerManager.instance.selectedTower != null)
        {
            TowerManager.instance.selectedTower.rangeModel.SetActive(false);
            TowerManager.instance.selectedTower = null;
        }

        TowerManager.instance.selectedTowerEffect.SetActive(false);

        notEnoughMoneyWarning.SetActive(false);
    }
}
