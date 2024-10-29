using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;

//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UIElements;

public class CameraBehaviour : MonoBehaviour
{
    //[ExecuteInEditMode]
    [SerializeField] GameObject ball;
    [SerializeField] float cameraSpeed;
    Camera myCamera;
    Vector2 windowSize;
    Vector2Int screenReference;
    Vector2Int res;
    Vector2 roomHalfSize;
    Vector2 targetSquare = new Vector2(0,0);
    Vector3 oldposition  = new Vector2(0,0);
    bool moving = false;
    float movementProgress= 0;
    bool stretch = false;
    [SerializeField] Material postProcessMaterial;
    //[SerializeField] RenderTexture rndrtxt;
    
    // Start is called before the first frame update
    void Start()
    {
        myCamera = gameObject.GetComponent<Camera>();
        //windowSize = new Vector2(myCamera.orthographicSize*myCamera.aspect, myCamera.orthographicSize);
        res = new Vector2Int(640,360);
        roomHalfSize = new Vector2(-1*transform.position.z, -1*transform.position.z/myCamera.aspect);
        windowSize =  new Vector2(res.x, res.y);
        screenReference = Screen.mainWindowPosition;
        
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
            transform.position = Vector3.Lerp(oldposition,SquareToPosition(targetSquare), progressRescaled);
            Screen.MoveMainWindowTo(Screen.mainWindowDisplayInfo,PositionToScreenPosition(transform.position)+screenReference);
            if (movementProgress == 1)
            {
                moving = false;
            }
            movementProgress += cameraSpeed*Time.deltaTime;
            movementProgress = math.clamp(movementProgress,0,1);
        }
        //else if (new Vector2Int(Screen.width,Screen.height)  != res){
            //Screen.SetResolution((int)math.ceil(res.x),(int)math.ceil(res.y),false);
            //Screen.MoveMainWindowTo(Screen.mainWindowDisplayInfo,PositionToScreenPosition(transform.position)+screenReference);
        //}
        else if (!stretch){
            screenReference = Screen.mainWindowPosition-PositionToScreenPosition(transform.position);
        }

    }

    Vector3 SquareToPosition(Vector2 square)
    {
        return new Vector3(square.x*roomHalfSize.x*2,square.y*roomHalfSize.y*2,transform.position.z);
    }

    Vector2Int PositionToScreenPosition(Vector3 pos){
        Vector2 tmp = new Vector2(pos.x,-1*pos.y);
        tmp = windowSize*tmp/(2*roomHalfSize);
        //tmp += screenReference;
        Vector2Int res = new Vector2Int((int)tmp.x, (int)tmp.y);
        return res;
    }

    /// <summary>
    /// OnRenderImage is called after all rendering is complete to render image.
    /// </summary>
    /// <param name="src">The source RenderTexture.</param>
    /// <param name="dest">The destination RenderTexture.</param>
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, postProcessMaterial);
        
    }


    public void Grow(){
        StartCoroutine(StretchResolution());
    }
    
    IEnumerator StretchResolution(){
        
        int w = Screen.width;
        int h = Screen.height;
        float z = transform.position.z;
        stretch = true;
        for (float i = 1.0f; i<=2.0f;i+=0.05f){
            transform.position =new Vector3(transform.position.x,transform.position.y, z*i);
            Vector2Int offset = new Vector2Int((int)math.ceil(w*i),(int)math.ceil(h*i)) - res;
            res = new Vector2Int((int)math.ceil(w*i),(int)math.ceil(h*i));
            //Screen.SetResolution((int)math.ceil(w*i),(int)math.ceil(h*i),false);
            screenReference-= offset/2;
            Screen.SetResolution((int)math.ceil(res.x),(int)math.ceil(res.y),false);
            Screen.MoveMainWindowTo(Screen.mainWindowDisplayInfo,PositionToScreenPosition(transform.position)+screenReference);
            //Screen.SetResolution((int)math.ceil(res.x),(int)math.ceil(res.y),false);
            //Screen.MoveMainWindowTo(Screen.mainWindowDisplayInfo,PositionToScreenPosition(transform.position)+screenReference);
            yield return new WaitForSeconds(0.05f);
        }
        stretch = false;
        

    }

    public void Warp(Vector3 pos){
        transform.position = pos;
        oldposition = Vector2.zero;
        screenReference = screenReference = Screen.mainWindowPosition-PositionToScreenPosition(transform.position);
    }
}