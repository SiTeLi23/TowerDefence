using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float range = 3f;
    public float fireRate;
    public LayerMask whatIsEnemy;
    private Collider[] colliderInRange;

    public List<EnemyController> enemiesInRange = new List<EnemyController>();

    private float checkCounter;
    public float checkTime = .2f;

    [HideInInspector]
    public bool enemiesUpdated;

    public GameObject rangeModel;

    public int cost = 100;
    [HideInInspector]
    public TowerUpgradeController upgrader;

    void Start()
    {
        checkCounter = checkTime;
        upgrader = GetComponent<TowerUpgradeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        #region Tower Checking Enemy In Range

        //each frame when the loop start, enemisUpdated should be set back to false
        enemiesUpdated = false;

        //we add a CD for tower to check enemy so it's not calling that check every frame which will make its computing less expensive
        checkCounter -= Time.deltaTime;
        if (checkCounter <= 0)
        {
            checkCounter = checkTime;

            //get all objects collider within the range
            colliderInRange = Physics.OverlapSphere(transform.position, range, whatIsEnemy);
            //make sure the list is empty at very start
            enemiesInRange.Clear();
            //add all enemies wihtin colliderInRange array into the enemy list
            foreach (Collider col in colliderInRange)
            {
                enemiesInRange.Add(col.GetComponent<EnemyController>());
            }

            enemiesUpdated = true;
        }

    #endregion

        if(TowerManager.instance.selectedTower == this) 
        {
            rangeModel.SetActive(true);
            rangeModel.transform.localScale = new Vector3(range, 1f, range);
        }

    }        

    private void OnMouseDown()
    {
        if (!LevelManager.instance.levelActive) return;

        if(TowerManager.instance.selectedTower != null) 
        {
            TowerManager.instance.selectedTower.rangeModel.SetActive(false);
        }

        TowerManager.instance.selectedTower = this;

        UIController.instance.OpenTowerUpGradePanel();

        TowerManager.instance.MoveTowerSelectionEffect();
    }
}
