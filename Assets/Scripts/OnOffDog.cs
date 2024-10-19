using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffDog : OnOff
{
    
    
    // Start is called before the first frame update
    public override void ChangeState(bool state){
        isItOn = state;
        GetComponent<DogControl>().ChangeEvilState(state);

    }
}
