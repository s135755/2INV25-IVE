using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveGoalPeg : MonoBehaviour {

    //Peg position
    float x, y, z;
    double movementFactor;

    void Start()
    {
        //Set y and movement factor constants
        y = transform.position.y;
        movementFactor = 1;
    }

    // Update is called once per frame
    void Update()
    {

        //Get mouse Position
        var mousePos = Input.mousePosition;
        //Transform position             
        var wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));

        //Get Schroll movement
        var d = Input.GetAxis("Mouse ScrollWheel");

        //Set x and z positions
        x = wantedPos.x;
        z = transform.position.z + (float)(d*movementFactor);
        
        //Move the peg
        transform.position = new Vector3(x, y, z);
        
    }
}
