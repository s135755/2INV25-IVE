using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveGoalPeg : MonoBehaviour {

    //Peg position
    float x, y, z;
    double movementFactor;

    // Goal plane
    public GameObject plane;

    void Start()
    {
        // Set y and movement factor constants
        y = transform.position.y;
        movementFactor = 1;
    }

    // Update is called once per frame
    void Update()
    {

        // Get mouse Position
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        // Get new peg position
        Vector3 pegPos = plane.GetComponent<PlaneFunctions>().getPegPointByMouseLocation(mousePos);
        
        // Move the peg
        transform.position = pegPos;
        
    }
}
