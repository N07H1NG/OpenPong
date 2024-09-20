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
    public bool bucket = false;
    AudioSource bounce;
    [SerializeField] float default_speed = 10;
    CircleCollider2D myCollider;
    Rigidbody2D myrigidBody;
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
        myrigidBody = GetComponent<Rigidbody2D>();
        myrigidBody.velocity = velocity;
        myrigidBody.WakeUp();
        //velocity = new Vector3(default_speed,0,0);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (velocity.magnitude != default_speed && force.magnitude == 0){
            float diff = (float)math.min(math.abs(velocity.magnitude-default_speed), 10*Time.deltaTime) * math.sign(velocity.magnitude-default_speed);
            velocity = velocity.normalized * (velocity.magnitude - diff);
            print("correcting");
        }
        else{
            print(velocity.magnitude);
        }
        velocity+= force*Time.deltaTime;
        myrigidBody.velocity = velocity;
        //transform.position += velocity*Time.deltaTime;
    }

    public void GetHit(Vector3 power)
    {
        print((1-math.dot(power.normalized,velocity.normalized)));
        velocity = (2*velocity+power*(1-math.dot(power.normalized,velocity.normalized))).normalized * velocity.magnitude;
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
        //ContactPoint2D contact = other.GetContact(0);
        //velocity = Vector3.Reflect(velocity,contact.normal);
        //bounce.Play();

    }

    public Vector3 GetVelocity()
    {
        return velocity;
    }

    public void CollisionRedefenition(Vector3 normal){
        velocity = Vector3.Reflect(velocity,normal);
        
    } 
}
