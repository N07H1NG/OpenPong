using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportBack : MonoBehaviour
{
    [SerializeField] CameraBehaviour cam;
    [SerializeField] AudioSource hole;
    // Start is called before the first frame update
    void OnTriggerExit2D(Collider2D other){
        if(other.TryGetComponent<BallBehaviour>(out BallBehaviour bh)){
            
            hole.spatialBlend = 1f;
            cam.Warp(cam.transform.position - Vector3.right*512f);
            print("aa");
            other.transform.position -= Vector3.right*512f;
        }
    }
}
