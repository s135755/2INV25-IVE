using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class ExperimentControl : MonoBehaviour {

    // Time and experiment parameters
    public static int INTROTIME = 5;
    public static int TIMEBETWEENEXP = 4;
    public static int NUMEXP = 10;
    public static int NUMTEST = 3;

    //Experiment counter
    int elapsedTests;
    //Data storag path relative to assets
    string relativePath = "/Resources/ExperimentData.txt";
    //Data storage path
    string path;

    //IOD and FOV values
    float iod, fov;

    //BlackScreen text
    public Text bsText;

    // Flag for storing data
    private bool STOREDATA = false;

    // Different states
    public enum State { INTRO, EXPIDLE, EXPRUNNING, FINISH, WAITING };

	// Keep track of time and state of experiments
	public State state;
    public int elapsedExp;
    public float startTime;

    // Use this for initialization
    void Start () {
        
        startTime = Time.time;
        elapsedExp = 0;
		state = State.INTRO;
        path = Application.dataPath + relativePath;
        //Since we are doing 3 training tests it will have values -2,-1 and 0 for them
        elapsedTests = 0;
        bsText = GameObject.Find("bsText").GetComponent<Text>();
        bsText.text = "This is Training Test " + (elapsedTests + 1);
        deleteDataFile();
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
            case State.WAITING:
                if (Time.time - startTime >= TIMEBETWEENEXP)
                {
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
                     if (elapsedTests >= NUMTEST)
                        STOREDATA = true;

                     finishTest(startTime,iod, fov);
                    }
                    
                break;
            case State.FINISH:
                blackScreen();
                break;
        }
    }

    // Finishes the test by storing all the given data
    void finishTest(float startTime, float iod, float fov)
    {
        // Keep count of tests
        if (elapsedTests < NUMTEST)
            elapsedTests++;
        
        if (STOREDATA) {
            float elapsedTime = Time.time - startTime;
           //Getting the variables, preparing the JSON and calling StoreData
            Vector2 distBetweenPegs = PegControl.getDistanceBetweenPegs();
            object[] args = new object[] { elapsedExp + 1, distBetweenPegs.x, distBetweenPegs.y, elapsedTime, fov, iod };
            string newData = "\"ExperimentData\": { \"Experiment\": " + args[0] + ", " +
                "\"XDistance\": " + args[1] +", " +
                "\"ZDistance\": " + args[2] + ", " +
                "\"Time\": " + args[3] + ", " +
                "\"FOV\": " + args[4] + ", " +
                "\"IOD\": " + args[5] + " }";/**/
            StoreData(newData);
            elapsedExp++;
        }
        blackScreen();

        //For the black screen to wait
        startTime = Time.time;
        state = State.WAITING;
    }

    // Sets up and starts the next test
    void startTest()
    {
        // TODO : Set different FOV and IOD values
        fov = Random.Range(-5, 5);
        iod = Random.Range(30, 180);
        
        // Put plegs in correct places
        PegControl.setPegsRandomly();

        // Set state to running and record starting time of test
        state = State.EXPRUNNING;
        startTime = Time.time;

        // Turn screen back to normal
        normalScreen();
    }

    // Puts the screen to black
    void blackScreen()
    {

        //Screen text message
        if (elapsedTests < NUMTEST )
            bsText.text = "This is Training Test " + (elapsedTests+1);
        else
        {
            if (elapsedExp < NUMEXP)
                bsText.text = "This is Experiment " + (elapsedExp+1);
            else
                bsText.text = "END";
        }
        //Turning screen black by switching from normal camara to black screen camara        
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled = false;
        GameObject.FindWithTag("CameraTwo").GetComponent<Camera>().enabled = true;
    }

    // Puts the screen to normal
    void normalScreen()
    {
        
        bsText.text = ""; 
        //Turning screen from black to normal 
        GameObject.FindWithTag("MainCamera").GetComponent<Camera>().enabled = true;
        GameObject.FindWithTag("CameraTwo").GetComponent<Camera>().enabled = false;
    }

    //Function to store the data of the experiment
    void StoreData(string newData)
    {
        string data = "";
        
        //If the file exists already we save the current data
        if (System.IO.File.Exists(path))
        {
            StreamReader inp_stm = new StreamReader(path);
            while (!inp_stm.EndOfStream)
            {
                string inp_ln = inp_stm.ReadLine().ToString();
                data += inp_ln + "\r\n";
            }

            inp_stm.Close();
        }
        string finalData = data + newData + "\r\n";
        System.IO.File.WriteAllText(path, finalData);
    }

   void deleteDataFile()
    {
        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        if (System.IO.File.Exists(path+".meta"))
        {
            System.IO.File.Delete(path+".meta");
        }
    }
}
