using LightJson;

public static class Blackboard
{
    public static PlayerControls ControlMap;

    public static PlayerControls ParseControlMap(PlayerControls newControls)
    {
        if(ControlMap != null)
        {
            return ControlMap;
        }

        return newControls;
    }
}
