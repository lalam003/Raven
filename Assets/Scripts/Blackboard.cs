public static class Blackboard
{
    // Static references
    public static PlayerControls ControlMap;
    public static Player Player;
    public static MainMenu Menu;

    public static PlayerControls ParseControlMap(PlayerControls newControls)
    {
        if (ControlMap != null)
        {
            return ControlMap;
        }
        ControlMap = newControls;

        return newControls;
    }
}
