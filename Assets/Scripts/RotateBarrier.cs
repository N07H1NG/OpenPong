using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class RotateControl : MonoBehaviour,IPush
{
    BoxCollider2D myCollider;
    [SerializeField] float speed;
    [SerializeField] float faloff = 2.5f;
    [SerializeField] float speedup = 2.5f;
    float progress = 0;
    float enemyDirection = 1;
    Quaternion startrot;
    Quaternion endrot;

    float mySpeed = 0;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        startrot = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        
        enemyDirection = math.sign(Input.GetAxis("Horizontal"));
        if (enemyDirection == 0)
        {
            mySpeed -= math.min(faloff*Time.deltaTime,math.abs(mySpeed))*math.sign(mySpeed);
            
        }
        else if (enemyDirection!= math.sign(mySpeed))
        {
            mySpeed = enemyDirection;
        }
        else
        {
            mySpeed += speedup*Time.deltaTime*enemyDirection;
        }
        progress += mySpeed*speed*Time.deltaTime;

        if (math.abs(progress) >= 0.5f)
        {
            mySpeed = 0;
            progress = math.clamp(progress,-0.5f,0.5f);
        }
        transform.rotation = Quaternion.Euler(0f,0f,math.lerp(45f,-45f,progress+0.5f))*startrot;
        print(progress+0.5f);
    }

    //public void PushMe(BallBehaviour plrComponent)
    //{
    //    plrComponent.GetHit(mySpeed*transform.up*speed*halfDistance);
    //}

    void IPush.PushMe(BallBehaviour ball){
        ball.GetHit(Vector3.zero);
    }
}
