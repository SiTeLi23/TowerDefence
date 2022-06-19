using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCam : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(transform.position + cam.transform.forward);

    }
}
