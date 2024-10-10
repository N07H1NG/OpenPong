using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    bool active = true;   
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
            if (active){
                GetComponent<AudioSource>().Play();
                Effect(plrComponent);
                active = false;
                StartCoroutine(Shrink());
                Destroy(gameObject,2.5f);
            }
        }
    }

    IEnumerator Shrink(){
        while (true){
            transform.localScale *=0.9f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public virtual void Effect(BallBehaviour plr){
        print("IDK");
    }
}