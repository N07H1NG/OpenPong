using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class waterlights : MonoBehaviour
{
    [SerializeField]GameObject lightObject;
    // Start is called before the first frame update
    void Start()
    {
        for (float i=-0.5f*transform.localScale.x; i<=0.5f*transform.localScale.x;i+=transform.localScale.x/math.floor(transform.localScale.x/30)){
            Vector3 spawnPoint =  transform.position + new Vector3(i,transform.localScale.y/2-20);
            Instantiate(lightObject,spawnPoint,Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
