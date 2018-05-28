using System.Collections;
using UnityEngine;

public class Camera : Singleton<Camera>
{
    [SerializeField]
    private Texture2D fadeTexture;
    private float fadeSpeed = 0.2f;
    private int drawDepth = -1000;

    private float alpha = 1.0f;
    private int fadeDir = -1;

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        GUI.depth = drawDepth;

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }

    [SerializeField]
    private float cameraTransitionSpeed = 1.0f;

    private IEnumerator currentRoutine;
    
    public bool MoveCamera(Vector2 position)
    {
        if((currentRoutine != null && !currentRoutine.MoveNext()) || currentRoutine == null)
        {
            currentRoutine = move(position);
            StartCoroutine(currentRoutine);

            return true;
        }

        return false;
    }

    IEnumerator move(Vector3 endPos)
    {
        Vector3 startPos = transform.position;
        Vector3 currentPos = startPos;
        float curr = 0.0f;
        endPos.z = transform.position.z;
        yield return null;
        while(currentPos != endPos)
        {
            curr += Time.deltaTime * cameraTransitionSpeed;
            currentPos = Vector3.Lerp(startPos, endPos, curr / 1.0f);
            transform.position = currentPos;
            yield return null;
        }
    }

    public IEnumerator fade()
    {
        yield return null;

        Blackboard.Player.PlayerMovement.CanMove = false;
        fadeDir = 1;
        while(alpha > 0) { yield return null; }
        Blackboard.Player.Respawn();
        fadeDir = -1;
        while(alpha < 1) { yield return null; }
        Blackboard.Player.PlayerMovement.CanMove = true;
    }
}
