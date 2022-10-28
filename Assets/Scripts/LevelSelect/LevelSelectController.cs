using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectController : MonoBehaviour
{
    public Transform theCam;

    public float moveSpeed;

    public Transform minPos, maxPos;
    void Start()
    {
        AudioManager.instance.PlayLevelSelectMusic();   
    }

    // Update is called once per frame
    void Update()
    {
        //get the percentage position of mouse position based on the screen size.
        Vector2 adjustedMousePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        //if the mouse is move on 75% of the scrren horinzontally, move camera to right.
        if(adjustedMousePos.x > .75f) 
        {
            if (adjustedMousePos.x > .9f)
            {
                theCam.position += theCam.right * moveSpeed * Time.deltaTime;
            }
            else 
            {
                theCam.position += theCam.right * moveSpeed * Time.deltaTime * .5f;
            }
        }

        //if the mouse is move on the 25% of the scrren horinzontally, move camera to left.
        if (adjustedMousePos.x < .25f)
        {
            if (adjustedMousePos.x > .1f)
            {
                theCam.position -= theCam.right * moveSpeed * Time.deltaTime;
            }
            else
            {
                theCam.position -= theCam.right * moveSpeed * Time.deltaTime * .5f;
            }
        }

        //if the mouse is move on 75% of the scrren vertically, move camera to up.
        if (adjustedMousePos.y > .75f)
        {
            if (adjustedMousePos.y > .9f)
            {
                theCam.position += theCam.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                theCam.position += theCam.forward * moveSpeed * Time.deltaTime * .5f;
            }
        }

        //if the mouse is move on the 25% of the scrren vertically, move camera to down.
        if (adjustedMousePos.y < .25f)
        {
            if (adjustedMousePos.y > .1f)
            {
                theCam.position -= theCam.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                theCam.position -= theCam.forward * moveSpeed * Time.deltaTime * .5f;
            }
        }

        //setting limitation for minpos and maxpos
        theCam.position = new Vector3(Mathf.Clamp(theCam.position.x,minPos.position.x, maxPos.position.x), theCam.position.y, Mathf.Clamp(theCam.position.z, minPos.position.z, maxPos.position.z));
    }
}
