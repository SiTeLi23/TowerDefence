using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int currentMoney;

    
    private void Awake()
    {
        #region Singleton

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        #endregion
    }

    private void Start()
    {
        UpdateMoneyUI();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void GiveMoney(int amountToGive)
    {
        currentMoney += amountToGive;
        UpdateMoneyUI();
    }

    public bool SpendMoney(int amountToSpend)
    {
        var canSpend = false;

        if (amountToSpend <= currentMoney)
        {
            canSpend = true;

            Debug.Log("Spend " + amountToSpend);
            currentMoney -= amountToSpend;
            UpdateMoneyUI();
        }

        return canSpend;
    }

    private void UpdateMoneyUI() 
    {
        UIController.instance.goldText.text = currentMoney.ToString();
    }
}