using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paint : Upgrade
{
    public override void Effect(BallBehaviour plr){
        plr.scoreLimit +=5;
    }
}
