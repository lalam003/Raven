using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEvent : EventTask
{
    [SerializeField]
    private float scrollSpeed, waitTime;

    [SerializeField]
    private DialogueSystem systemA, systemB;

    protected const string fileName = "BaseJson.json";
    protected const string keyNameA = "personA";
    protected const string keyNameB = "personB";

    public override void ExecuteTask()
    {
        StartCoroutine(cutscene(keyNameA));
        StartCoroutine(cutscene(keyNameB));
    }

    private IEnumerator cutscene(string keyName)
    {
        yield return null;
        systemA.DisplayText(fileName, keyName);
        do
        {
            while (systemA.DialogueRunning)
            {
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        } while (!systemA.BreakDialogue());
    }
}
