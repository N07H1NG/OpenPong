using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class FlyRandomly : MonoBehaviour
{
    Vector3 velocity;
    Vector3 axis1;
    Vector3 axis2;
    [SerializeField]float speed;
    [SerializeField]Vector2 xborders;
    [SerializeField]Vector2 yborders;
    [SerializeField]Vector2 zborders;
    Vector2[] borders;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Random.onUnitSphere;
        axis1 = Random.onUnitSphere;
        axis2 = Random.onUnitSphere;
        velocity *= speed;
        //borders = new Vector2[xborders,yborders,zborders];
        borders = new Vector2[] {xborders,yborders,zborders};
        for (int i = 0; i<3; i++){
            borders[i].x += transform.position[i];
            borders[i].y += transform.position[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position+= velocity*Time.deltaTime;
        transform.Rotate(axis1,Time.deltaTime*120);
        axis1 = Quaternion.AngleAxis(Time.deltaTime*50,axis2) * axis1;

        for (int i = 0; i<3; i++){
            if(ShouldBounce(transform.position[i],velocity[i],borders[i].x, borders[i].y)){
                velocity[i] *= -1;
            }
        }


    }

    bool ShouldBounce(float a,float vel, float l, float h){
        return ((a<l || a>h) && math.sign(vel) != math.sign(math.abs(a-h)-math.abs(a-l)));
    }
}
