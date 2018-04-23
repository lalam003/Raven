using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum Direction
    {
        North = 0,
        South,
        East,
        West
    }

    [SerializeField]
    private const float moveDistance = 1.0f;

    private Direction playerFacingDirection;

    private PlayerControls playerControls;

    private IEnumerator inputBuffer;

    private Vector3 targetPos;

    private void Awake()
    {
        playerControls = Blackboard.ParseControlMap(playerControls);
    }

    private void FixedUpdate()
    {
        targetPos = gameObject.transform.position;
        if(Input.GetKey(playerControls.North))
        {
            targetPos.x += moveDistance;
        }
        if (Input.GetKey(playerControls.South))
        {
            targetPos.x += (-1f * moveDistance);
        }
        if (Input.GetKey(playerControls.East))
        {
            targetPos.y += moveDistance;
        }
        if (Input.GetKey(playerControls.West))
        {
            targetPos.y += (-1f * moveDistance);
        }
        if (Input.GetKey(playerControls.North))
        {
            //Interact
        }
        if (targetPos != gameObject.transform.position)
        {
            if (inputBuffer == null)
            {
                StartCoroutine(move(targetPos));
            }
            else
            {
                inputBuffer = move(targetPos);
            }
        }
    }

    private IEnumerator move(Vector2 targetPos)
    {
        yield return null;
        Vector2 startingPos = gameObject.transform.position;
        Vector2 currPos = startingPos;
        float timeElapsed = 0;
        while(currPos != targetPos)
        {
            timeElapsed += Time.deltaTime;
            currPos = Vector2.Lerp(startingPos, targetPos, timeElapsed / 1.0f);
            gameObject.transform.position = currPos;
            yield return null;
        }
        if(inputBuffer != null)
        {
            StartCoroutine(inputBuffer);
            inputBuffer = null;
        }
    }

}
