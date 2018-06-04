using UnityEngine;

public class PlayerControls
{
    public enum Controls
    {
        North = 0,
        South,
        East,
        West,
        Interact,
        Menu
    }

    public KeyCode North { get; private set; }
    public KeyCode South { get; private set; }
    public KeyCode East { get; private set; }
    public KeyCode West { get; private set; }
    public KeyCode Interact { get; private set; }
    public KeyCode Menu { get; private set; }

    public PlayerControls()
    {
        InitializeDefaults();
    }

    public void InitializeDefaults()
    {
        North    = KeyCode.W;
        South    = KeyCode.S;
        East     = KeyCode.D;
        West     = KeyCode.A;
        Interact = KeyCode.Space;
        Menu     = KeyCode.Escape;
    }

    public void SetKey(Controls control, KeyCode newInput)
    {
        switch (control)
        {
            case Controls.North:
                North = newInput;
                break;
            case Controls.South:
                South = newInput;
                break;
            case Controls.East:
                East = newInput;
                break;
            case Controls.West:
                West = newInput;
                break;
            case Controls.Interact:
                Interact = newInput;
                break;
            case Controls.Menu:
                Menu = newInput;
                break;
            default:
                Debug.LogWarning("Control " + control + " not recognized.");
                break;
        }
    }
}
