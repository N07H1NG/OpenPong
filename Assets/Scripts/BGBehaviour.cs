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
    BallBehaviour ballComp;
    Vector3 lastVel;
    // Start is called before the first frame update
    void Start()
    {
        ballComp = Ball.GetComponent<BallBehaviour>();
        lastVel = new Vector3 (1,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetVel = ballComp.GetVelocity()/50;
        lastVel = Vector3.MoveTowards(lastVel, targetVel,Time.deltaTime*5);
        transform.up = -1*lastVel;
    }
}
