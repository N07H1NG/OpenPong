using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockedwall : MonoBehaviour
{
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour ball)){
            GetComponent<AudioSource>().Play();

        }
    }

    public void Open(){
        GetComponent<Collider2D>().enabled = false;
    }
}
