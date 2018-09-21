using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentControl : MonoBehaviour {

	// Time parameters
	public static int INTROTIME = 5;
	public static int EXPTIME = 10;

	// Different states
	public enum State {INTRO, EXP, FINISH};

	// Keep track of time and state
	public double elapsedTime;
	public State state;

	// Use this for initialization
	void Start () {
		elapsedTime = 0;
		state = State.INTRO;
	}
	
	// Update is called once per frame
	void Update () {

		// Increment time counter
		elapsedTime += Time.deltaTime;

		// TODO: for each state, do stuff
		switch(state)
		{
			case State.INTRO:
				// Show dialog with experiment explanation maybe?
				break;
			case State.EXP:
				// Construct a certain number of alignment tests and show them
				break;
			case State.FINISH:
				// Remove everything from display, maybe display dialogue
				break;
		}
	}
}
