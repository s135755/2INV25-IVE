using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentControl : MonoBehaviour {

	// Time and experiment parameters
	public static int INTROTIME = 5;
	public static int TIMEBETWEENEXP = 2;
    public static int NUMEXP = 10;


    // Flag for storing data
    private bool STOREDATA = true;

	// Different states
	public enum State {INTRO, EXPIDLE, EXPRUNNING, FINISH};

	// Keep track of time and state of experiments
	public State state;
    public int elapsedExp;
    public float startTime;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        elapsedExp = 0;
		state = State.INTRO;
	}

    // Update is called once per frame
    void Update()
    {
        // For each state, do stuff
        switch (state)
        {
            case State.INTRO:
                blackScreen();
                if (Time.time - startTime >= INTROTIME) {
                    state = State.EXPIDLE;
                }
                break;
            case State.EXPIDLE:
                if (elapsedExp == NUMEXP) {
                    state = State.FINISH;
                } else {
                    startTest();
                }
                break;
            case State.EXPRUNNING:
                if (Input.GetMouseButtonDown(0))
                    {
                        // TODO: obtain data
                        finishTest(new Vector2(0,0), 0, 0, 0);
                    }
                break;
            case State.FINISH:
                blackScreen();
                break;
        }
    }

    // Finishes the test by storing all the given data
    void finishTest(Vector2 pegDist, float time, float iod, float fov)
    {
        blackScreen();
        if (STOREDATA) {
            // TODO : I/O  all data to text file
        }

        // Leave screen black for some amount of time
        wait(TIMEBETWEENEXP);
        state = State.EXPIDLE;
    }

    // Sets up and starts the next test
    void startTest()
    {
        // TODO : Set different FOV and IOD values
        // TODO : Put plegs in correct places
        
        // Set state to running and record starting time of test
        state = State.EXPRUNNING;
        startTime = Time.time;

        // Turn screen back to normal
        normalScreen();
    }

    // Puts the screen to black
    void blackScreen()
    {
        //Turning screen black by switching from normal camara to black screen camara        
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled = false;
        GameObject.FindWithTag("CameraTwo").GetComponent<Camera>().enabled = true;
    }

    // Puts the screen to normal
    void normalScreen()
    {
        //Turning screen from black to normal 
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        GameObject.FindWithTag("CameraTwo").GetComponent<Camera>().enabled = false;
    }

    //Wait function to sleep thread
    IEnumerator wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
