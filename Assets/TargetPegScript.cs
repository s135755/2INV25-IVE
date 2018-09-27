using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPegScript : MonoBehaviour {

    //X variable range from center
    int xRange = 5;
    //X variable range from center
    int zRange = 5;

    //Variables to store Target and Goal planes position
    Vector3 TargetPosition;

    //Function to get a random position within the planes
    Vector3 getRandomPos()
    {
        float x = Random.Range(-xRange, xRange);
        float y = TargetPosition.y;
        float z = Random.Range(-zRange, zRange);
        return new Vector3(x, y, z);
    }
    //Function to set the values for the planes position
    void setPlanePosition()
    {
        TargetPosition = GameObject.Find("TargetPlane").transform.position;
    }

    void Start()
    {
        setPlanePosition();
        //Randomly set the peg starting position
        transform.position = getRandomPos();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

