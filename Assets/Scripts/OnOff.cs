using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    [SerializeField]public bool isItOn = false;
    [SerializeField] Color onColor;
    [SerializeField] Color offColor;
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(isItOn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void ChangeState(bool state){
        isItOn = state;
        GetComponent<BarrierControl>().enabled = isItOn;
        GetComponent<Collider2D>().enabled = isItOn;
        if(isItOn){
            GetComponentInChildren<MeshRenderer>().material.SetColor("_Color",new Color(0.7346249f,0,1,1.0f));
            
            //GetComponent<SpriteRenderer>().color = new Color(0.3064703f,0.9150943f,0.4862813f,1);
        }
        else{
            GetComponentInChildren<MeshRenderer>().material.SetColor("_Color",new Color(0.2f,0.2f,0.2f,0.2f));
            //GetComponent<SpriteRenderer>().color = new Color(0.2f,0.2f,0.2f,0.2f);
        }
    }

}
