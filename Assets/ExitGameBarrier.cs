using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent<BallBehaviour>(out BallBehaviour bh)){
            yield return new WaitForSeconds(2f);
            Application.Quit(); 
        }
               
    }
}
