using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class Hole : MonoBehaviour
{
    bool active = false;
    BallBehaviour ball;
    bool applied = false;
    float timer = 0;
    float radius;
    [SerializeField] Vector3 choice;
    Vector3 current_force = Vector3.zero;
    // Start is called before the first frame update
    
    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<BallBehaviour>(out BallBehaviour brvr)){
            ball = brvr;
            active = true;
            timer = 0f;
            radius = (transform.position - ball.transform.position).magnitude;
        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent<BallBehaviour>(out BallBehaviour brvr)){
            ball = brvr;
            active = false;
            
        }
    }

    // <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (active){
            timer += 1f*Time.deltaTime;
            if (applied){
                ball.force -= current_force;
            }
            Vector3 d = transform.position - ball.transform.position;
            d.z=0;
            float p = math.clamp((GetComponent<Collider2D>().bounds.extents.y-d.magnitude)/(GetComponent<Collider2D>().bounds.extents.y),0f,1f);
            print(p);
            current_force = d.normalized*0.15f*(GetComponent<Collider2D>().bounds.extents.y)/d.magnitude*d.magnitude;
            
            current_force += 0.2f*d*(0.5f-Vector3.Dot(ball.GetVelocity().normalized,d.normalized));
            current_force -= 0.4f*math.pow(p,2.7f)*Vector3.ProjectOnPlane(ball.GetVelocity(),d.normalized);
            ball.force += current_force;
            applied = true;
            if (d.magnitude < 3f){
                StartCoroutine(Teleport());
            }
            GetComponent<AudioSource>().spatialBlend = 1.2f-p;
        }
        else{
            if(applied){
                ball.force -= current_force;
                applied = false;
            }
        }
        
    }

    IEnumerator Teleport(){
        ball.transform.position = choice;
        ball.SetVelocity(ball.GetVelocity().normalized*ball.default_speed);
        ball.UpdateScore(0);
        active = false;
        yield return null;
    }
}
