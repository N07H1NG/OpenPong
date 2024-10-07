using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : Upgrade
{
    [SerializeField] GameObject Cam;

    // Start is called before the first frame update


    public override void Effect(BallBehaviour plr){
        CameraBehaviour bh = Cam.GetComponent<CameraBehaviour>();
        bh.Grow();
    }
}
