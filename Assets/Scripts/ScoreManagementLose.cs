using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
[ExecuteInEditMode]
public class ScoreManagementLose : MonoBehaviour
{
    
    [SerializeField] int limit;
    AudioSource beep;
    AudioSource glass;
    AudioSource water;
    bool flyThrough;
    // Start is called before the first frame update
    void Start()
    {
        
        if (limit <= 5){
            
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.red);
        }
        else if (limit <=10){
            
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.green);
            
        }
        else if (limit<=15){
            GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor",Color.blue);
            
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
                //plrComponent.score = math.max(plrComponent.score-1,0);
                beep.Play();
                Vector3 sidevector = plrComponent.olderposition-gameObject.transform.position;
                float side = math.dot(transform.right,sidevector);
                side = math.sign(side);
                plrComponent.CollisionRedefenition(transform.right*side);
                plrComponent.score = 0;
            }
            
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent)){
            if (flyThrough){
                flyThrough = false;
                glass.Play();
                plrComponent.score -= limit;
                plrComponent.score = math.max(plrComponent.score,0);
            }
        }
        
    }
}
