using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    
    [SerializeField] Vector3 myForce;
    BoxCollider2D myBox;
    AudioSource windAudio;


    // Start is called before the first frame update
    void Start()
    {
        windAudio = GetComponents<AudioSource>()[0];

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
        if (other.TryGetComponent<BallBehaviour>(out BallBehaviour bhvr)){
            bhvr.force += myForce;
            windAudio.Play();
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<BallBehaviour>(out BallBehaviour bhvr)){
            bhvr.force -= myForce;
            windAudio.Stop();
        }
    }
}
