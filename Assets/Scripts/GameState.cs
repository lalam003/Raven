using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Make a Singleton
public class GameState : MonoBehaviour
{
    public enum States { Overworld, Menu, Dialogue, Cutscene };

    public States State
    {
        get
        {
            return state;
        }

        set
        {

        }
    }
    private States state;

    private void Awake()
    {
        // Set initial state
        State = States.Overworld;
    }

    /// <summary>
    /// Attempt to switch the game state, on success return true.
    /// Usage: if (GameState.Instance.ToOverworld()) { ... }
    /// </summary>
    public bool ToOverworld()
    {
        if (state != States.Overworld)
        {
            state = States.Overworld;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Attempt to switch the game state, on success return true.
    /// Usage: if (GameState.Instance.ToDialogue()) { ... }
    /// </summary>
    public bool ToMenu()
    {
        if (state == States.Overworld)
        {
            state = States.Menu;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Attempt to switch the game state, on success return true.
    /// Usage: if (GameState.Instance.ToDialogue()) { ... }
    /// </summary>
    public bool ToDialogue()
    {
        if (state == States.Overworld)
        {
            state = States.Dialogue;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Attempt to switch the game state, on success return true.
    /// Usage: if (GameState.Instance.ToDialogue()) { ... }
    /// </summary>
    public bool ToCutscene()
    {
        if (state == States.Overworld)
        {
            state = States.Cutscene;
            return true;
        }
        return false;
    }

    public bool IsOverworld()
    {
        return (state == States.Overworld);
    }

    public bool IsMenu()
    {
        return (state == States.Menu);
    }

    public bool IsDialogue()
    {
        return (state == States.Dialogue);
    }

    public bool IsCutscene()
    {
        return (state == States.Cutscene);
    }
}
