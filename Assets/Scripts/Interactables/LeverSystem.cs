using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSystem : EventTask
{
    [SerializeField]
    private List<Lever> levers;
    [SerializeField]
    private List<bool> winState, loseState;

    public override void ExecuteTask()
    {
        bool win = true;
        bool lose = true;
        for(int i = 0; i < levers.Count; ++i)
        {
            win &= (levers[i].On == winState[i]);
            if(loseState.Count != 0)
            {
                lose &= (levers[i].On == loseState[i]);
            }
        }
        if(win)
        {
            gameObject.SetActive(false);
        }
        else if(lose)
        {
            Blackboard.Player.PlayerDeath();
        }
    }
}
