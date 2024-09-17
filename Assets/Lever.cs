using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    bool leverState = false;
    [SerializeField] GameObject controlled;
    [SerializeField] AudioClip onSound;
    [SerializeField] AudioClip offSound;
    OnOff controlledOnOff;
    AudioSource mySrc;
    // Start is called before the first frame update
    void Start()
    {
        controlledOnOff = controlled.GetComponent<OnOff>();
        mySrc = GetComponent<AudioSource>();
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

        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent)){
            leverState = !leverState;
            if (leverState){
                mySrc.clip = onSound;
            }
            else{
                mySrc.clip = offSound;
            }
            mySrc.Play();
            controlledOnOff.ChangeState(leverState);
            transform.Rotate(Vector3.up,180);
        }    
    }
}
