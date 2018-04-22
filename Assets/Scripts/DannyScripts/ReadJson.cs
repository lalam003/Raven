using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadJson : MonoBehaviour {

    string jsonString;
	// Use this for initialization
	void Start () {
        jsonString = File.ReadAllText(Application.dataPath + "/Resources/Readthis.txt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
