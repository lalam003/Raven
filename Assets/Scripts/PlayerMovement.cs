using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
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
    private float moveSpeed = 1.0f;

    private const string NorthKey = "Up";
    private const string SouthKey = "Down";
    private const string EastKey = "Right";
    private const string WestKey = "Left";
    private const string IdleKey = "Idle";

    private Direction playerFacingDirection;

    private PlayerControls playerControls;

    private Animator anim;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerControls = Blackboard.ParseControlMap(new PlayerControls());
    }

    private void FixedUpdate()
    {
        Vector2 targetPos = Vector2.zero;
        Direction targetDir = playerFacingDirection;

        if (Input.GetKey(playerControls.West))
        {
            targetPos.x += moveSpeed;
            targetDir = Direction.West;
        }
        if (Input.GetKey(playerControls.East))
        {
            targetPos.x += (-1f * moveSpeed);
            targetDir = Direction.East;
        }
        if (Input.GetKey(playerControls.North))
        {
            targetPos.y += moveSpeed;
            targetDir = Direction.North;
        }
        if (Input.GetKey(playerControls.South))
        {
            targetPos.y += (-1f * moveSpeed);
            targetDir = Direction.South;
        }

        if (Input.GetKey(playerControls.Interact))
        {
            //@todo: Implement Interaction here
        }

        if (targetPos != Vector2.zero)
        {
            if (targetDir != playerFacingDirection || anim.GetBool(IdleKey))
            {
                playerFacingDirection = targetDir;
                string key = getAnimationKey();
                anim.SetBool(IdleKey, false);
                anim.SetTrigger(key);
            }
        }
        else
        {
            anim.SetBool(IdleKey, true);
        }

        rb.velocity = targetPos;
    }

    private string getAnimationKey()
    {
        switch (playerFacingDirection)
        {
            case Direction.North:
                return NorthKey;
            case Direction.South:
                return SouthKey;
            case Direction.East:
                return EastKey;
            case Direction.West:
                return WestKey;
            default:
                Debug.LogError("Direction Not Found");
                return "";
        }
    }
}
