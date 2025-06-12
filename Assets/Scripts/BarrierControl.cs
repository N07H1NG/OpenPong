using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Timeline;

public class BarrierControl : MonoBehaviour,IPush
{
    BoxCollider2D myCollider;
    [SerializeField] float halfDistance;
    [SerializeField] float speed;
    [SerializeField] float faloff = 2.5f;
    [SerializeField] float speedup = 2.5f;
    float progress = 0;
    float enemyDirection = 1;
    Vector3 endpoint1;
    Vector3 endpoint2;
    public GameObject ballObject;
    float mySpeed = 0;
    bool usePulleys = false;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        endpoint1 = transform.position - transform.up*halfDistance;
        endpoint2 = transform.position + transform.up*halfDistance;
        if (PulleyInput.Instance != null){
            usePulleys = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!usePulleys)
        {
            enemyDirection = GetInputDirection();

            if (enemyDirection == 0)
            {
                mySpeed -= math.min(faloff * Time.deltaTime, math.abs(mySpeed)) * math.sign(mySpeed);

            }
            else if (enemyDirection != math.sign(mySpeed))
            {
                mySpeed = enemyDirection;
            }
            else
            {
                mySpeed += speedup * Time.deltaTime * enemyDirection;
            }
            progress += mySpeed * speed * Time.deltaTime;
        }
        else
        {
            float targ = (PulleyInput.Instance.AvgValue - 30f) / (85f - 30f) - 0.5f;
            mySpeed = (targ - progress) / (speed * Time.deltaTime);
            progress = targ;
        }
        if (math.abs(progress) >= 0.5f)
        {
            mySpeed = 0;
            progress = math.clamp(progress, -0.5f, 0.5f);
        }
        transform.position = Vector3.Lerp(endpoint1,endpoint2,progress+0.5f);
    }

    void IPush.PushMe(BallBehaviour plrComponent)
    {
        plrComponent.GetHit(mySpeed*transform.up*speed*halfDistance);
    }

    float GetInputDirection()
    {
        if (usePulleys){
            return PulleyInput.Instance.GetPulleyDirection();
        }
        return math.sign(Input.GetAxis("Horizontal"));
    }
}
