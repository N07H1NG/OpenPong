using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterscript : MonoBehaviour
{
    [SerializeField] BallBehaviour ball;
    AudioSource audioComp;
    // Start is called before the first frame update
    void Start()
    {
        audioComp = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ball.transform.position.x,transform.position.y,transform.position.z);
    }
}
