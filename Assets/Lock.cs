using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] GameObject key;
    bool open = false;
    [SerializeField] GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<BallBehaviour>(out BallBehaviour ball)&& !open){
            if (ball.key){
                open = true;
                key.GetComponent<MeshRenderer>().enabled = true;
                GetComponentInChildren<Animation>().Play();
                GetComponent<AudioSource>().Play();
                wall.GetComponent<lockedwall>().Open();
            }

        }
    }
}
