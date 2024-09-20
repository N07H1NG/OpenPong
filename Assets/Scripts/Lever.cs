using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Lever : MonoBehaviour
{
    bool leverState = false;
    [SerializeField] GameObject[] controlled;
    [SerializeField] AudioClip onSound;
    [SerializeField] AudioClip offSound;
    OnOff[] controlledOnOff;
    AudioSource mySrc;
    // Start is called before the first frame update
    void Start()
    {
        controlledOnOff = new OnOff[controlled.Length];
        mySrc = GetComponent<AudioSource>();
        for(int i = 0; i < controlled.Length;i++){
            //OnOff tmp = controlled[i].GetComponent<OnOff>();
            controlledOnOff[i] =controlled[i].GetComponent<OnOff>();
        }

        for(int i = 0; i < controlled.Length;i++){
            //OnOff tmp = controlled[i].GetComponent<OnOff>();
            print(controlledOnOff[i]);
            //print(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        print("HIIIT");
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent)){
            leverState = !leverState;
            if (leverState){
                mySrc.clip = onSound;
            }
            else{
                mySrc.clip = offSound;
            }
            mySrc.Play();
            foreach (OnOff elem in controlledOnOff){
                elem.ChangeState(leverState);
            }
            transform.Rotate(Vector3.up,180);
        }    
    }
}
