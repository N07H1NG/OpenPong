using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting; 
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using UnityEditor;
using System;
public class MyIntEvent : UnityEvent <int> {}


public class BallBehaviour : MonoBehaviour
{
    public static MyIntEvent scareEvent = new MyIntEvent();

    //public Vector3 oldposition;
    //public Vector3 olderposition;
    public int score = 0;
    Vector3 velocity;
    [SerializeField]public bool greenBucket = false;
    [SerializeField]public bool blueBucket = false;
    AudioSource bounce;
    public int scoreLimit = 5;
    public bool infinity = false;
    public bool key = false;

    public int passing = 0;
    [SerializeField] float default_speed = 10;

    Rigidbody2D myRigidBody;
    public Vector3 force = new Vector3(0,0,0);
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(640,360,false);
        bounce = GetComponent<AudioSource>();
        float ang = math.radians(Random.Range(45,-45));
        velocity = new Vector3(math.cos(ang), math.sin(ang), 0) * default_speed;
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.velocity = velocity;
        myRigidBody.WakeUp();
        //velocity = new Vector3(default_speed,0,0);
        //score = 30;
    }

    // Update is called once per frame
     /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        if (velocity.magnitude != default_speed){
            float diff = (float)math.min(math.abs(velocity.magnitude-default_speed), 5*Time.fixedDeltaTime) * math.sign(velocity.magnitude-default_speed);
            velocity = velocity.normalized * (velocity.magnitude - diff);
        }
        if (passing==0){
            velocity+= force*Time.fixedDeltaTime;
        }
        myRigidBody.velocity = velocity;
        //olderposition = oldposition;

        //oldposition = transform.position;

        
    }
    void Update()
    {
        //oldposition = transform.position;
        
        //transform.position += velocity*Time.deltaTime;

        if (Input.GetKeyDown("space")){
            score = scoreLimit;
            scareEvent.Invoke(score);
        }
    }

    public void GetHit(Vector3 power)
    {
        velocity = (2.1f*velocity+power*(1-math.dot(power.normalized,velocity.normalized))).normalized * math.max(velocity.magnitude,default_speed);
        score +=1;
        score = math.min(score,scoreLimit);
        scareEvent.Invoke(score);
        bounce.Play();
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    { 
        ContactPoint2D contact = other.GetContact(0);
        //throw new System.Exception();
        Vector3 n = Vector3.Project(transform.position - new Vector3(contact.point.x,contact.point.y,0),contact.normal).normalized;
        CollisionRedefenition(n);
        if (other.gameObject.TryGetComponent<IPush>(out IPush ip)){
            ip.PushMe(this);
        }
        
        
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void CollisionRedefenition(Vector3 normal){
       
        if(math.dot(velocity,normal)<=0){
            normal = Vector3.Project(-1*velocity,normal).normalized;
            Vector3 reflected = Vector3.Reflect(velocity,normal).normalized;
            if (Vector3.Project(reflected,normal).magnitude <0.1f){
                reflected = (Vector3.ProjectOnPlane(reflected,normal) + normal*0.1f).normalized;
            }
            velocity = reflected*math.max(velocity.magnitude,default_speed);
            GetComponents<AudioSource>()[1].Play();
        }
    } 
}
