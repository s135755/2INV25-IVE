using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickedAction : MonoBehaviour {

    //Wait time until the screen turns on again
    private int waitTime;
    //Control when click action should be managed
    private bool clickEnabled;


    void Start()
    {
        //Turn on Main Camera
        NormalScreen();
        //Initialize time variable
        waitTime = 5;
    }
       
    void Update()
    {
        //Check for mouse acction
        MouseClicked();
    }

    void NextTest()
    {
        SaveTestData();
        //TODO: Loading Next Test

    }
    void SaveTestData()
    {
        //TODO: Save current test data
    }

    //Mouse clicked managment
    void MouseClicked()
    {
        //If left clicked and we should manage the click
        if (Input.GetMouseButtonDown(0) && clickEnabled)
        {
            clickEnabled = false;
            //Turn screen black
            BlackScreen();
        }
    }

    //Turning screen black
    void BlackScreen()
    {
        //Turning screen black by switching from normal camara to black screen camara        
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled = false;
        GameObject.FindWithTag("CameraTwo").GetComponent<Camera>().enabled = true;
        
        //Coroutine that waits to turn screen to normal again after "waitTime" seconds
        StartCoroutine(waitToNormalScreen());

        //Next test call
        NextTest();
    }
           
    //Function to switch to normal screen
    void NormalScreen()
    {
        //Turning screen from black to normal 
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        GameObject.FindWithTag("CameraTwo").GetComponent<Camera>().enabled = false;
        
        //Enable the click so the next test can be ended
        clickEnabled = true;
    }

    //Wait function that turns screen on
    IEnumerator waitToNormalScreen()
    {
        yield return new WaitForSeconds(waitTime);
        NormalScreen();
    }


}

