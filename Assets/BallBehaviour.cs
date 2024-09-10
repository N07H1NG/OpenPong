using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
public class BallBehaviour : MonoBehaviour
{
    Vector3 velocity;
    [SerializeField] float default_speed = 10;
    CircleCollider2D myCollider;
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
        //float ang = math.radians(Random.Range(0,360));
        //velocity = new Vector3(math.cos(ang), math.sin(ang), 0) * default_speed;
        velocity = new Vector3(default_speed,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity*Time.deltaTime;
    }

    public void GetHit(Vector3 power)
    {
        velocity += power;
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contact = other.GetContact(0);
        velocity = Vector3.Reflect(velocity,contact.normal);

    }
}
