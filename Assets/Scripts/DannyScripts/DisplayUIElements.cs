using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayUIElements : MonoBehaviour {

    // Use this for initialization
    // Displays the current time elapsed.
    Text timeDisplay;
    TimeSystem timeSys;
    private void Awake()
    {
        timeDisplay = GetComponent<Text>();
        timeSys = new TimeSystem();
    }
	
	// Update is called once per frame
	private void Update () {
        timeDisplay.text = timeSys.GetTime().ToString();
	}
}
