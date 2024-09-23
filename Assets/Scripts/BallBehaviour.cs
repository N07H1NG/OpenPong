using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;
public class BallBehaviour : MonoBehaviour
{
    public int score;
    Vector3 velocity;
    [SerializeField]public bool greenBucket = false;
    [SerializeField]public bool blueBucket = false;
    AudioSource bounce;
    [SerializeField] float default_speed = 10;
    CircleCollider2D myCollider;
    Rigidbody2D myRigidBody;
    public Vector3 force = new Vector3(0,0,0);
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        myCollider = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bounce = GetComponent<AudioSource>();
        float ang = math.radians(Random.Range(45,-45));
        velocity = new Vector3(math.cos(ang), math.sin(ang), 0) * default_speed;
        myRigidBody = GetComponent<Rigidbody2D>();
        myRigidBody.velocity = velocity;
        myRigidBody.WakeUp();
        //velocity = new Vector3(default_speed,0,0);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(velocity.magnitude);
        if (velocity.magnitude != default_speed){
            float diff = (float)math.min(math.abs(velocity.magnitude-default_speed), 5*Time.deltaTime) * math.sign(velocity.magnitude-default_speed);
            velocity = velocity.normalized * (velocity.magnitude - diff);
        }
        velocity+= force*Time.deltaTime;
        myRigidBody.velocity = velocity;
        //transform.position += velocity*Time.deltaTime;
    }

    public void GetHit(Vector3 power)
    {
        //print((1-math.dot(power.normalized,velocity.normalized)));
        //print(power);
        velocity = (1.5f*velocity+power*(1-math.dot(power.normalized,velocity.normalized))).normalized * math.max(velocity.magnitude,default_speed);
        score +=1;
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
        CollisionRedefenition(contact.normal);
    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void CollisionRedefenition(Vector3 normal){
        normal = Vector3.Project(-1*velocity,normal).normalized;
        Vector3 reflected = Vector3.Reflect(velocity,normal).normalized;
        if (Vector3.Project(reflected,normal).magnitude <0.1f){
            reflected = (Vector3.ProjectOnPlane(reflected,normal) + normal*0.1f).normalized;
        }
        velocity = reflected*math.max(velocity.magnitude,default_speed);
        
    } 
}
