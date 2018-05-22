using System;
using System.Collections;
using System.Collections.Generic;
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

    public delegate Vector2 MovementAction();
    public MovementAction Up;
    public MovementAction Down;
    public MovementAction Left;
    public MovementAction Right;

    public delegate void ControlsAction();
    public ControlsAction Menu;
    public ControlsAction Interact;

    [SerializeField]
    private float moveSpeed = 1.0f;

    [SerializeField]
    private float interactDistance = 1f;
    [SerializeField]
    private const int interactableLayerMask = 8;

    private const string NorthKey = "Up";
    private const string SouthKey = "Down";
    private const string EastKey = "Right";
    private const string WestKey = "Left";
    private const string IdleKey = "Idle";

    private Direction playerFacingDirection;
    private PlayerControls playerControls;
    private Animator anim;
    private Rigidbody2D rb;
    private bool canMove = true;
    public bool CanMove
    {
        get
        {
            return canMove;
        }

        set
        {
            rb.velocity = Vector2.zero;
            anim.SetBool(IdleKey, true);
            canMove = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerControls = Blackboard.ParseControlMap(new PlayerControls());
        interactDistance = 1; //GridClass.Instance.GetComponent<Grid>.cellsize.x
        ResetMovementKeys();
    }

    private void FixedUpdate()
    {
        if(CanMove)
        {
            Vector2 targetPos = Vector2.zero;
            Direction prevDir = playerFacingDirection;

            if (Input.GetKey(playerControls.West))
            {
                targetPos = Left();
            }
            if (Input.GetKey(playerControls.East))
            {
                targetPos = Right();
            }
            if (Input.GetKey(playerControls.North))
            {
                targetPos = Up();
            }
            if (Input.GetKey(playerControls.South))
            {
                targetPos = Down();
            }

            if (targetPos != Vector2.zero)
            {
                if (prevDir != playerFacingDirection || anim.GetBool(IdleKey))
                {
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
    }

    private void Update()
    {
        if (CanMove)
        {
            if (Input.GetKeyDown(playerControls.Interact))
            {
                Interact();
            }
            if (Input.GetKeyDown(playerControls.Menu))
            {
                Menu();
            }
        }
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
                Debug.LogError("Direction type " + playerFacingDirection + " not a valid direction");
                return "";
        }
    }

    private bool interact()
    {   
        Vector2 start = transform.position;
        Vector2 end = start + getInteractDirection() * interactDistance;
        int layerMask = 1 << interactableLayerMask;

        RaycastHit2D[] hitObjects = Physics2D.LinecastAll(start, end, layerMask);

        if (hitObjects.Length > 0)
        {
            // Get all objects in front of the player with the layerMask
            foreach (RaycastHit2D hit in hitObjects)
            {
                // Get the first hit object in the list which has an Interactable script and interact with it
                Interactable hitInteractable = hit.transform.GetComponent<Interactable>();

                if (hitInteractable)
                {
                    hitInteractable.Interact();
                    return true;
                }
            }
        }
        return false;
    }

    private Vector2 getInteractDirection()
    {
        switch (playerFacingDirection)
        {
            case Direction.North:
                return Vector2.up;
            case Direction.South:
                return Vector2.down;
            case Direction.East:
                return Vector2.right;
            case Direction.West:
                return Vector2.left;
            default:
                Debug.LogError("Direction type " + playerFacingDirection + " not a valid direction");
                return Vector2.zero;
        }
    }

    public void ResetMovementKeys()
    {
        Left = () =>
        {
            playerFacingDirection = Direction.West;
            return Vector2.left * moveSpeed;
        };

        Right = () =>
        {
            playerFacingDirection = Direction.East;
            return Vector2.right * moveSpeed;
        };

        Up = () =>
        {
            playerFacingDirection = Direction.North;
            return Vector2.up * moveSpeed;
        };

        Down = () =>
        {
            playerFacingDirection = Direction.South;
            return Vector2.down * moveSpeed;
        };

        Menu = () =>
        {
            if (!Blackboard.Menu.gameObject.activeSelf)
            {
                Blackboard.Menu.gameObject.SetActive(true);
            }
        };

        Interact = () =>
        {
            interact();
        };
    }

    public static IEnumerator HeldKeyStart(KeyCode key, Action callback, float delay)
    {
        float startTime = Time.unscaledTime;
        KeyCode heldKey = key;
        // Wait one second before quick-scrolling
        while (Input.GetKey(key))
        {
            // wait 1 second before quick-toggling
            if (Time.unscaledTime - startTime < 0.5f)
            {
                yield return null;
            }
            else
            {
                // continue toggling 5 times per second
                callback();
                yield return new WaitForSecondsRealtime(delay);
            }
        }
    }
}
