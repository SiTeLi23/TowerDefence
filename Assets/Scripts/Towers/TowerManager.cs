using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    //current selected tower
    public Tower activeTower;

    //where can we place the tower
    public Transform indicator;

    //we are currently in placing tower mode
    public bool isPlacing;

    //top 15 percent of the screen should be a safe area.
    public float topSafePercent = 15f;

    public LayerMask whatIsPlacment, whatIsObstacle;


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
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlacing)
        {
            //the fake tower which haven't been built
            indicator.position = GetGridPosition();

            RaycastHit hit;

            //make sure the the 15% screen height at top will be safe click place
            if (Input.mousePosition.y > Screen.height * (1f - topSafePercent / 100f))
            {
                indicator.gameObject.SetActive(false);
            }
            else if (Physics.Raycast(indicator.position + new Vector3(0f, -2f, 0f), Vector3.up, out hit, 10f,
                         whatIsObstacle))
            {
                //if the indicator is overlap with an obstacle, do not show indicator
                indicator.gameObject.SetActive(false);
            }
            else
            {
                indicator.gameObject.SetActive(true);

                //if we don't have enough money, we show the warning
                UIController.instance.notEnoughMoneyWarning.SetActive(MoneyManager.instance.currentMoney < activeTower.cost);

                //if you click left mouse button, you can instantiate tower model
                if (Input.GetMouseButtonDown(0))
                    if (MoneyManager.instance.SpendMoney(activeTower.cost))
                    {
                        isPlacing = false;

                        Instantiate(activeTower, indicator.position, activeTower.transform.rotation);

                        indicator.gameObject.SetActive(false);

                        UIController.instance.notEnoughMoneyWarning.SetActive(false);
                    }
            }
        }
    }

    public void StartTowerPlacement(Tower towerToPlace)
    {
        activeTower = towerToPlace;

        isPlacing = true;

        Destroy(indicator.gameObject);

        var placeTower = Instantiate(activeTower);
        placeTower.enabled = false; // make sure the tower not attacking when hovering but not build yet
        placeTower.GetComponent<Collider>().enabled = false;
        indicator = placeTower.transform;

        placeTower.rangeModel.SetActive(true);
        placeTower.rangeModel.transform.localScale = new Vector3(placeTower.range, 1f, placeTower.range);
    }


    public Vector3 GetGridPosition()
    {
        var location = Vector3.zero;

        //get the mouse selected position
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, whatIsPlacment)) location = hit.point;

        location.y = 0f;


        return location;
    }
}