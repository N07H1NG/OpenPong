using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public class ScoreManagementLose : MonoBehaviour
{
    
    [SerializeField] int limit;
    [SerializeField] AudioClip infsound;
    AudioSource beep;
    AudioSource glass;
    AudioSource water;
    bool flyThrough;
    bool scared;
    void IAmScared(int score)
    {
        scared = score>=limit;
        if(scared){
            GetComponent<Collider2D>().isTrigger = true;
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Shake",1.0f);
        }
        else{
            GetComponent<Collider2D>().isTrigger = false;
            GetComponentInChildren<MeshRenderer>().material.SetFloat("_Shake",0.0f);
        }

        if (score >=30){
            glass.clip = infsound;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        BallBehaviour.scareEvent.AddListener(IAmScared);
        GetComponentInChildren<MeshRenderer>().material.EnableKeyword("_EMISSION");
        if (limit <= 5){
            
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.red/3);
        }
        else if (limit <=10){
            
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.green/3);
            
        }
        else if (limit<=15){
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.blue/3);
            
        }
        beep = GetComponents<AudioSource>()[0];
        glass = GetComponents<AudioSource>()[1];
        water = GetComponents<AudioSource>()[2];
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent))
        {
            
            if (plrComponent.score >= limit){
                flyThrough = true;
            }
            else{
                //
                //eep.Play();
                //ector3 sidevector = plrComponent.olderposition-gameObject.transform.position;
                //loat side = math.dot(transform.right,sidevector);
                //ide = math.sign(side);
                //lrComponent.CollisionRedefenition(transform.right*side);
                //lrComponent.score = 0;
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent)){
            if (flyThrough){
                flyThrough = false;
                glass.Play();
                if (!plrComponent.infinity){
                    plrComponent.score -= limit;
                    plrComponent.score = math.max(plrComponent.score,0);
                }
                
                BallBehaviour.scareEvent.Invoke(plrComponent.score);
            }
        }
        
    }


    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent))
        {
                beep.Play();
                
                plrComponent.score = 0;
                BallBehaviour.scareEvent.Invoke(0);
        }

    }

    
}
