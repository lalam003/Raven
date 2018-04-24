using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuAddons {

    // Use this for initialization
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void NewMenuOption()
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("Assets/Create/Create Json")]
    private static void CreateJson()
    {
        // Create and add a new default Json for testing and other things.
    }
}
