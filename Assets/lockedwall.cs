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
            GetComponentInChildren<Animation>().Play("Hit");

        }
    }

    public void StartOpen(){
        StartCoroutine(Open());
    }
    IEnumerator Open(){
        yield return new WaitForSeconds(4.2f);
        GetComponentInChildren<Animation>()["Open"].speed = 0.5f;
        GetComponentInChildren<Animation>().Play("Open");
        yield return new WaitForSeconds(0.2f);
        GetComponent<Collider2D>().enabled = false;
    }
}
