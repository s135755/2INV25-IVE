using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegControl : MonoBehaviour {

    

    //Variables to store Target and Goal planes position
    Vector3 TargetPosition, GoalPosition;

    //Peg position variables
    float x, y, z;
    double movementFactor;

    // Gameobjects
    public GameObject goalPlane, targetPlane;
    public GameObject targetPeg, goalPeg;

    //X variable range from center
    int xRange = 5;
    //X variable range from center
    int zRange = 5;

    //Function to get a random position within the planes
    Vector3 getRandomPos(Vector3 position)
    {
        float x = Random.Range(-xRange, xRange);
        float y = position.y;
        float z = Random.Range(-zRange, zRange);
        return new Vector3(x, y, z);
    }
    //Function to set the values for the planes position
    void setPlanePosition()
    {
            TargetPosition = targetPlane.transform.position;
            GoalPosition = goalPlane.transform.position;
    }

    //Function that returns the X and  Z distance between two pegs
    public Vector2 getDistanceBetweenPegs()
    {
        float xdist = Mathf.Abs(goalPeg.transform.position.x - targetPeg.transform.position.x);
        float zdist = Mathf.Abs(goalPeg.transform.position.z - targetPeg.transform.position.z);
        return new Vector2(xdist,zdist);
    }

    void assignGameObjects()
    {
        goalPlane = GameObject.Find("GoalPlane");
        targetPlane = GameObject.Find("TargetPlane");
        goalPeg = GameObject.Find("GoalPeg");
        targetPeg = GameObject.Find("TargetPeg");
        
}

    // Use this for initialization
    void Start () {

        assignGameObjects();

        setPlanePosition();
        
        //Randomly set the peg starting position
        targetPeg.transform.position = getRandomPos(TargetPosition);
        goalPeg.transform.position = getRandomPos(GoalPosition);

        // Set y and movement factor constants
        y = transform.position.y;
        movementFactor = 1;
    }
	
	// Update is called once per frame
	void Update () {
        // Get mouse Position
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Get new peg position
        Vector3 pegPos = goalPlane.GetComponent<PlaneFunctions>().getPegPointByMouseLocation(mousePos);

        // Move the peg
        goalPeg.transform.position = pegPos;
    }
}
