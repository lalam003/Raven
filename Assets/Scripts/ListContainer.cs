using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// This class contains a text array which shows up in the inspector,
/// make an array or list of this class to make a 2D array that shows up in the inspector.
/// </summary>
[System.Serializable]
public class TextArrayContainer
{
    public List<Text> innerList;
}