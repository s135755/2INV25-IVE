using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegControl : MonoBehaviour {

    
    //Peg position variables
    public float x, y, z;

    // Gameobjects
    public static GameObject goalPlane, targetPlane;
    public static GameObject targetPeg, goalPeg;

    //Function to get a random position within the planes
    public static Vector3 getRandomPos(GameObject plane)
    {
        Vector3 planeCenter = plane.transform.position;
        Vector3 extent = plane.GetComponent<Renderer>().bounds.extents;
        Vector3 position = new Vector3(extent.x/2, planeCenter.y, extent.z/2);

        float x = Random.Range(-position.x, position.x);
        float y = position.y;
        float z = Random.Range(-position.z, position.z);
        return new Vector3(x, y, z);
    }

    //Function that returns the X and  Z distance between two pegs
    public static Vector2 getDistanceBetweenPegs()
    {
       // GameObject goalPeg = GameObject.Find("GoalPeg");
       // GameObject targetPeg = GameObject.Find("TargetPeg");
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
        
        
        //Randomly set the peg starting position
        targetPeg.transform.position = getRandomPos(targetPlane);
        goalPeg.transform.position = getRandomPos(goalPlane);

        // Set y and movement factor constants
        y = transform.position.y;
    }
	
    public static void setPegsRandomly()
    {
        //Randomly set the peg starting position
        targetPeg.transform.position = getRandomPos(targetPlane);
        goalPeg.transform.position = getRandomPos(goalPlane);
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
    public static void hidePegs(bool hide)
    {
        goalPeg.SetActive(!hide);
        targetPeg.SetActive(!hide);
    }
}
