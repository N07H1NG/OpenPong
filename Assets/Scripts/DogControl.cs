using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class DogControl : MonoBehaviour
{
    public bool keyInPlace = true;
    public bool triggered = false;
    [SerializeField] GameObject mouth;
    Vector3 direction = new Vector3 (1,0,0);
    [SerializeField]float speed = 6;
    [SerializeField] GameObject ball;
    Animator myAnimator;
    [SerializeField] List<Material> nicematerial ;
    [SerializeField] List<Material> evilmaterial;
    [SerializeField] GameObject Key;
    bool evilstate = true;
    bool jump = false;
    public Vector3 respawn;
    Vector3 velocity;
    public bool following = true;
    public bool carrying = false;
    public Vector3 point;
    bool jumpDelay;
    [SerializeField] GameObject spawn;
    //Vector3 playerCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        respawn = transform.position;
        //playerCheckpoint = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {   if (!triggered && following){
            return;
        }
        if (following){
            point = ball.transform.position;
        }
        else{
            if ((new Vector2(transform.position.x,transform.position.y) - new Vector2(point.x,point.y)).magnitude <= 1){
                if (carrying){
                    ball.transform.SetParent(null,true);
                    ball.GetComponent<Rigidbody2D>().WakeUp();
                    ball.GetComponent<CircleCollider2D>().enabled = true;
                    ball.GetComponent<BallBehaviour>().enabled = true;
                    ball.transform.position= new Vector3(ball.transform.position.x,ball.transform.position.y,0);
                    following = false;
                    carrying = false;
                    triggered = false;
                    point = respawn;
                    //point = new Vector3(UnityEngine.Random.Range(-90f,154f),UnityEngine.Random.Range(348,444f),0);
                }
                else{
                    if(!keyInPlace){
                        Instantiate(Key,respawn,Quaternion.identity);
                        keyInPlace = true;
                    }
                    following = true;
                }
                
            }
        }
        if (!jump){
            direction = point - transform.position;
            direction.y = 0;
            speed = 12;
            float diff = point.y - transform.position.y;
            if (math.abs(diff) >= 1 && !jumpDelay){
                StartCoroutine(Jump(transform.position.y+math.clamp(diff,-15f,15f)));
            }
            direction.z = 0;
            direction.Normalize();
            velocity = direction * speed;
        }
        transform.position += velocity * Time.deltaTime;
        float turnamount = math.max(20-math.abs(point.x - transform.position.x), 0);
        transform.LookAt(turnamount*new Vector3(0,0,1)+new Vector3((2*transform.position -point).x,transform.position.y,transform.position.z));
        
    }

    public void ChangeEvilState(bool target)
    {
        if (evilstate != target){
            evilstate = target;
            SkinnedMeshRenderer dogmesh = GetComponentInChildren<SkinnedMeshRenderer>();
            if (evilstate){
                GetComponent<AudioSource>().Play();
                dogmesh.materials[0].color = Color.black;
                dogmesh.materials[1].color = Color.red;
                dogmesh.materials[1].SetColor("_EmissionColor",Color.red);
            }
            else{
                GetComponent<AudioSource>().Stop();
                dogmesh.materials[0].color = Color.white;
                dogmesh.materials[1].color = Color.blue;
                dogmesh.materials[1].SetColor("_EmissionColor",Color.blue);
            }
            
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(following && other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent))
        {
            carrying = true;
            
            following = false;
            point = spawn.transform.position;
            other.gameObject.transform.position = mouth.transform.position;
            other.gameObject.transform.SetParent(mouth.transform,true);
            other.gameObject.GetComponent<Rigidbody2D>().Sleep();
            other.enabled = false;
            if (plrComponent.key){
                plrComponent.key = false;
                keyInPlace = false;
            }
            plrComponent.enabled = false;
            //point = new Vector3(UnityEngine.Random.Range(-90f,154f),UnityEngine.Random.Range(348f,444f),0);
            //other.gameObject.transform.position = playerCheckpoint;
            //Vector3 aboba = new Vector3(UnityEngine.Random.Range(-224f,288f),UnityEngine.Random.Range(-270f,474),5 );
            //gameObject.transform.position = respawn;
            //ChangeEvilState(!evilstate);
        }

    }


    IEnumerator Jump(float target){
        //print("im jumba");
        myAnimator.SetBool("Jumping", true);
        jump = true;
        yield return new WaitForSeconds(0.22f);
        float vertVel;
        bool startArc;
        if (target >= transform.position.y){
            vertVel = 2.4f*(target-transform.position.y);
            startArc = true;
        }
        else{
            vertVel = 35f;
            startArc = true;
        }

        while (startArc||(transform.position.y >= target && !startArc )){
            vertVel -= 85f*Time.deltaTime;
            if (vertVel <= 0){
                startArc = false;
            }
            velocity.y = vertVel;
            if (!startArc){
                float v = -vertVel;
                float a = 85f;
                float h = math.abs(transform.position.y-target);
            
                float solve = (-v+math.sqrt(v*v+2*a*h))/a;
                if(solve<0.2f){
                    myAnimator.SetBool("Jumping", false);
                }
                //myAnimator.SetBool("Jumping", false);
            }
            yield return null;
        }
        myAnimator.SetBool("Jumping", false);
        jump = false;
        jumpDelay = true;
        float timing = math.clamp((math.abs(point.x-transform.position.x)/(1+ math.abs(point.y-transform.position.y)/15f))/speed,0.1f,5f);
        yield return new WaitForSeconds(timing);
        jumpDelay = false;
    }
}
