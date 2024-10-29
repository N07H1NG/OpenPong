using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Hole : MonoBehaviour
{
    bool active = false;
    BallBehaviour ball;
    bool applied = false;
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
            if (applied){
                ball.force -= current_force;
            }
            Vector3 d = transform.position - ball.transform.position;
            current_force = 3*d.normalized*math.pow(d.magnitude,0.5f);
            ball.force += current_force;
            applied = true;
        }
        else{
            if(applied){
                ball.force -= current_force;
                applied = false;
            }
        }
        
    }
}
