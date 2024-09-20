using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up,Time.deltaTime*90);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent)){
            plrComponent.bucket = true;
            plrComponent.GetComponent<SpriteRenderer>().color = new Color(0,0,1,1);
            GetComponent<AudioSource>().Play();
        }
    }
}
