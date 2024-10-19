using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : Upgrade
{
    // Start is called before the first frame update
    public override void Effect(BallBehaviour plr){
        plr.key = true;
    }
    
}
