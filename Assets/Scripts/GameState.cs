public class GameState : Singleton<GameState>
{
    public enum States {
        Overworld = 0,
        Menu,
        Dialogue,
        Cutscene };

    private States state;
    public States State
    {
        get
        {
            return state;
        }
    }

    private void Awake()
    {
        // Set initial state
        state = States.Overworld;
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
}
