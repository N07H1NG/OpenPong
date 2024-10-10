using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infinity : Upgrade
{
    public override void Effect(BallBehaviour plr){
        plr.scoreLimit = 30;
        plr.score = 30;
        plr.infinity = true;
        BallBehaviour.scareEvent.Invoke(30);
    }
}
