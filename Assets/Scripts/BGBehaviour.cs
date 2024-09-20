using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class BGBehaviour : MonoBehaviour
{
    [SerializeField]GameObject Ball;
    [SerializeField]GameObject Cam;
    Camera CamComp;
    MeshRenderer myMeshRenderer;
    BallBehaviour ballComp;
    Vector3 lastVel;
    // Start is called before the first frame update
    void Start()
    {
        CamComp = Cam.GetComponent<Camera>();
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        ballComp = Ball.GetComponent<BallBehaviour>();
        lastVel = new Vector3 (1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetVel = ballComp.GetVelocity()/50;
        lastVel = Vector3.MoveTowards(lastVel, targetVel,Time.deltaTime*5);
        transform.rotation = Quaternion.LookRotation(lastVel,Vector3.back);
    }
}
