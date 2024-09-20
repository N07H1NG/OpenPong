using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManagementLose : MonoBehaviour
{
    [SerializeField] int limit;
    AudioSource beep;
    AudioSource glass;
    AudioSource water;
    [SerializeField]bool iAmRed=true;
    // Start is called before the first frame update
    void Start()
    {
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
            
            if ((plrComponent.score >= limit) && (plrComponent.bucket||iAmRed)){
                if (!plrComponent.bucket){
                    glass.Play();
                }
                else{
                    water.Play();
                }
            }
            else{
                //plrComponent.score = math.max(plrComponent.score-1,0);
                beep.Play();
                plrComponent.CollisionRedefenition(transform.right);
            }
            plrComponent.score = 0;
        }
    }
}
