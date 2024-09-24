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
    bool iAmGreen=false;
    bool iAmBlue=false;
    bool flyThrough;
    // Start is called before the first frame update
    void Start()
    {
        
        if (limit <= 5){
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (limit <=10){
            GetComponent<SpriteRenderer>().color = Color.green;
            iAmGreen = true;
        }
        else if (limit<=15){
            GetComponent<SpriteRenderer>().color = Color.blue;
            iAmBlue = true;
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
            
            if ((plrComponent.score >= limit)&&(!iAmGreen||plrComponent.greenBucket)&&(!iAmBlue||plrComponent.blueBucket)){
                flyThrough = true;
            }
            else{
                //plrComponent.score = math.max(plrComponent.score-1,0);
                beep.Play();
                plrComponent.CollisionRedefenition(transform.right);
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
