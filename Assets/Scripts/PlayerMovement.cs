﻿using System;
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

    [SerializeField]
    private float InteractDistance = 1f;
    [SerializeField]
    private int InteractableLayerMask = 8;

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

    private void Start()
    {
        SetInteractDistance();
    }

    private void FixedUpdate()
    {
        Vector2 targetPos = Vector2.zero;
        Direction targetDir = playerFacingDirection;

        if (Input.GetKey(playerControls.West))
        {
            targetPos.x += (-1f * moveSpeed);
            targetDir = Direction.West;
        }
        if (Input.GetKey(playerControls.East))
        {
            targetPos.x += moveSpeed;
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

    private void Update()
    {
        if (Input.GetKeyDown(playerControls.Interact))
        {
            Interact();
        }
    }

    private void SetInteractDistance()
    {
        InteractDistance = 1; //GridClass.Instance.GetComponent<Grid>.cellsize.x;
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
    
    private void Interact()
    {
        Vector2 start = transform.position;
        Vector2 end = start + GetInteractDirection() * InteractDistance;

        int layerMask = 1 << InteractableLayerMask;

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
                }
            }
        }
    }

    private Vector2 GetInteractDirection()
    {
        switch (playerFacingDirection)
        {
            case Direction.North:
            {
                return Vector2.up;
            }
            case Direction.South:
            {
                return Vector2.down;
            }
            case Direction.East:
            {
                return Vector2.right;
            }
            case Direction.West:
            {
                return Vector2.left;
            }
            default:
            {
                return Vector2.down;
            }
        }
    }

}
