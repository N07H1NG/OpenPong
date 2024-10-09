using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffLight : OnOff
{
    
    [SerializeField]bool isItOn = false;
    // Start is called before the first frame update
    public override void ChangeState(bool state){
        isItOn = state;
        GetComponent<Light>().enabled = !isItOn;
    }
}
