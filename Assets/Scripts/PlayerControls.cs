using UnityEngine;

public class PlayerControls
{
    public enum Controls
    {
        North = 0,
        South,
        East,
        West,
        Interact
    }

    public KeyCode North { get; private set; }
    public KeyCode South { get; private set; }
    public KeyCode East { get; private set; }
    public KeyCode West { get; private set; }
    public KeyCode Interact { get; private set; }

    public PlayerControls()
    {
        InitializeDefaults();
    }

    public void InitializeDefaults()
    {
        North    = KeyCode.W;
        South    = KeyCode.S;
        East     = KeyCode.A;
        West     = KeyCode.D;
        Interact = KeyCode.Space;
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
            default:
                break;
        }
    }
}
