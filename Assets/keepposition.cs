using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepposition : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,20);
    }
}
