using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;


    private void Awake()
    {
        #region Singleton
        if(instance != null) 
        {
            Destroy(gameObject);
           
        }
        else 
        {
             instance = this;
        }
        #endregion
    }

    //current selected tower
    public Tower activeTower;
    //where can we place the tower
    public Transform indicator;
    //we are currently in placing tower mode
    public bool isPlacing;

    public LayerMask whatIsPlacment;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing) 
        {
            indicator.position = GetGridPosition();

            if (Input.GetMouseButtonDown(0)) 
            {
                isPlacing = false;

                Instantiate(activeTower, indicator.position, activeTower.transform.rotation);

                indicator.gameObject.SetActive(false);
            }
        }
    }

    public void StartTowerPlacement(Tower towerToPlace) 
    {
        activeTower = towerToPlace;

        isPlacing = true;

        Destroy(indicator.gameObject);

        Tower placeTower = Instantiate(activeTower);
        placeTower.enabled = false; // make sure the tower not attacking when hovering but not build yet
        indicator = placeTower.transform;
    }


    public Vector3 GetGridPosition()
    {

        Vector3 location = Vector3.zero;

        //get the mouse selected position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 200f, whatIsPlacment )) 
        {
            location = hit.point;
        
        }

        location.y = 0f;


        return location;
    }
}
