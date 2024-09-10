using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float cameraSpeed;
    BallBehaviour ballData;
    Camera myCamera;
    Vector2 windowSize;
    Vector2 roomHalfSize;
    Vector2 targetSquare = new Vector2(0,0);
    Vector3 oldposition  = new Vector2(0,0);
    bool moving = false;
    float movementProgress= 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ballData = ball.GetComponent<BallBehaviour>();
        myCamera = gameObject.GetComponent<Camera>();
        windowSize = new Vector2(myCamera.orthographicSize*myCamera.aspect, myCamera.orthographicSize);
        print(windowSize);
        roomHalfSize = windowSize;
    }

    // Update is called once per frame
    void Update()
    {
        float targetSquareX = math.floor((ball.transform.position.x + roomHalfSize.x) / (roomHalfSize.x*2));
        float targetSquareY = math.floor((ball.transform.position.y + roomHalfSize.y) / (roomHalfSize.y*2));
        if (targetSquare != new Vector2(targetSquareX,targetSquareY))
        {
            moving = true;
            movementProgress = 0f;
            oldposition = transform.position;
            targetSquare = new Vector2(targetSquareX,targetSquareY);
        }
        if (moving)
        {
            float progressRescaled = MathHelper.EaseInOutBesier(movementProgress);
            print(progressRescaled);
            transform.position = Vector3.Lerp(oldposition,SquareToPosition(targetSquare), progressRescaled);
            print(oldposition);
            if (movementProgress == 1)
            {
                moving = false;
            }
            movementProgress += cameraSpeed*Time.deltaTime;
            movementProgress = math.clamp(movementProgress,0,1);
        }
    }

    Vector3 SquareToPosition(Vector2 square)
    {
        return new Vector3(square.x*roomHalfSize.x*2,square.y*roomHalfSize.y*2,transform.position.z);
    }
}
