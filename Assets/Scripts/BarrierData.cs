using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BarrierData : MonoBehaviour
{
    BoxCollider2D myCollider;
    [SerializeField] float halfDistance;
    [SerializeField] float speed;
    float progress = 0;
    float enemyDirection = 1;
    Vector3 endpoint1;
    Vector3 endpoint2;
    public GameObject ballObject;
    // Start is called before the first frame update
     /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        endpoint1 = transform.position - transform.up*halfDistance;
        endpoint2 = transform.position + transform.up*halfDistance;

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        progress += enemyDirection*speed*Time.deltaTime;
        if (math.abs(progress) >= 0.5 && math.sign(progress) == math.sign(enemyDirection))
        {
            enemyDirection *= -1;
        }
        transform.position = Vector3.Lerp(endpoint1,endpoint2,progress+0.5f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent))
        {
            plrComponent.GetHit(math.sign(enemyDirection)*transform.up*speed);
        }

    }
}
