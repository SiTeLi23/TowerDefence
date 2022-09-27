using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
