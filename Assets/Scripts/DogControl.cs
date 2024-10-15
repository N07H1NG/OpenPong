using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class DogControl : MonoBehaviour
{
    Vector3 direction = new Vector3 (1,0,0);
    [SerializeField]float speed = 8;
    [SerializeField] GameObject ball;
    float speed_scale = 6.5f;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.speed = speed/speed_scale;
    }

    // Update is called once per frame
    void Update()
    {
        
        direction = (ball.transform.position - transform.position);
        direction.z = 0;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
        float turnamount = math.max(20-(ball.transform.position - transform.position).magnitude, 0);
        transform.LookAt(turnamount*new Vector3(0,0,1)+2*transform.position -ball.transform.position);
    }
}
