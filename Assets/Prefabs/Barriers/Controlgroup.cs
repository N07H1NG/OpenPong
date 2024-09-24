using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlgroup : MonoBehaviour
{
    bool groupState = false;
    [SerializeField] GameObject[] controlled;
    OnOff[] controlledOnOff;
    [SerializeField] GameObject[] controllers;
    Lever[] controllerScripts;
    AudioSource mySrc;
    [SerializeField] AudioClip onSound;
    [SerializeField] AudioClip offSound;
    // Start is called before the first frame update
    void Start()
    {
        controlledOnOff = new OnOff[controlled.Length];
        mySrc = GetComponent<AudioSource>();
        for(int i = 0; i < controlled.Length;i++){
            //OnOff tmp = controlled[i].GetComponent<OnOff>();
            controlledOnOff[i] =controlled[i].GetComponent<OnOff>();
        }
        controllerScripts = new Lever[controllers.Length];
        for(int i = 0; i < controllers.Length;i++){
            controllerScripts[i] = controllers[i].GetComponent<Lever>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GroupChangeState(){
        groupState = !groupState;
            if (groupState){
                mySrc.clip = onSound;
            }
            else{
                mySrc.clip = offSound;
            }
            mySrc.Play();
            foreach (OnOff elem in controlledOnOff){
                elem.ChangeState(groupState);
            }
            
            foreach(Lever lvr in controllerScripts){
                lvr.ReceiveChange(groupState);
            }
    }
}
