using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]Vector3 myForce = new Vector3(0,0,0); 
    [SerializeField]GameObject splashPrefab;
    BoxCollider2D myBox;
    AudioSource waterAudio;
    AudioSource splashAudio;
    GameObject mySplash;
    ParticleSystem splashSystem;
    [SerializeField] MyAudioCue splashCue;
    // Start is called before the first frame update
    void Start()
    {
        waterAudio = GetComponents<AudioSource>()[0];
        splashAudio = GetComponents<AudioSource>()[1];
        mySplash = Instantiate(splashPrefab);
        splashSystem = mySplash.GetComponent<ParticleSystem>();
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
            Vector3 vel = -1*bhvr.GetVelocity();
            mySplash.transform.position = other.transform.position-vel*Time.deltaTime;
            Quaternion rot = Quaternion.LookRotation(vel,Vector3.Cross(Vector3.up,vel));
            mySplash.transform.rotation = rot;
            splashSystem.Play(); 
            bhvr.force += myForce;
            float vol = vel.magnitude/32;
            splashAudio.volume = vol;
            waterAudio.Play();
            splashAudio.PlayOneShot(splashCue.GetRandomClip());
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
            Vector3 vel = bhvr.GetVelocity();
            mySplash.transform.position = other.transform.position-vel*Time.deltaTime;
            
            Quaternion rot = Quaternion.LookRotation(vel,Vector3.Cross(Vector3.up,vel));
            mySplash.transform.rotation = rot;
            splashSystem.Play();  
            bhvr.force -= myForce;
            float vol = vel.magnitude/32;
            splashAudio.volume = vol;
            waterAudio.Stop();
            splashAudio.PlayOneShot(splashCue.GetRandomClip());
        }
    }
}
