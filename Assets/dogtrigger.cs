using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogtrigger : MonoBehaviour
{
    [SerializeField]GameObject dog;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (other.TryGetComponent<BallBehaviour>(out BallBehaviour ball)){
            dog.GetComponent<DogControl>().triggered = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<BallBehaviour>(out BallBehaviour ball)){
            if (!ball.key){
                DogControl d = dog.GetComponent<DogControl>();
                d.following = false;
                if (!d.carrying){
                    d.point = d.respawn;
                }
                d.triggered = false;
            }
            
        }
    }
}
