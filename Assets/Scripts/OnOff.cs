using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[ExecuteInEditMode]
public class OnOff : MonoBehaviour
{
    [SerializeField]bool isItOn = false;
    [SerializeField] Color onColor;
    [SerializeField] Color offColor;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BarrierControl>().enabled = isItOn;
        GetComponent<Collider2D>().enabled = isItOn;
        if(isItOn){
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        }
        else{
            GetComponent<SpriteRenderer>().color = new Color(0.2f,0.2f,0.2f,0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(bool state){
        isItOn = state;
        GetComponent<BarrierControl>().enabled = isItOn;
        GetComponent<Collider2D>().enabled = isItOn;
        if(isItOn){
            GetComponent<SpriteRenderer>().color = new Color(0.3064703f,0.9150943f,0.4862813f,1);
        }
        else{
            GetComponent<SpriteRenderer>().color = new Color(0.2f,0.2f,0.2f,0.2f);
        }
    }

}
