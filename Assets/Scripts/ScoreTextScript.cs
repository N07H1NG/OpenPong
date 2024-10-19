using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField] GameObject PlayerObject;
    BallBehaviour ball;
    TextMeshProUGUI tM;
    float timer;
    Color baseColor;
    [SerializeField]Texture2D rainbow;
    // Start is called before the first frame update
    void Start()
    {
        ball = PlayerObject.GetComponent<BallBehaviour>();
        tM = gameObject.GetComponent<TextMeshProUGUI>();
        baseColor = tM.color;
    }

    Color SineColorMix(Color col1,Color col2,float var, float speed){
        return col1*(1+math.sin(math.PI/2+speed*math.radians(var)))*0.5f + col2*(1-math.sin(math.PI/2+speed*math.radians(var)))*0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ball.infinity){
            tM.text = ball.score.ToString();
            
        }
        else{
            tM.text = "∞";
            tM.transform.localScale = new Vector3(2,2,2);
        }
        if (ball.score >=5){
            timer += Time.deltaTime*60;
            timer%=360;
            if (true){
                
                Color flashcolor = Color.red;
                if (ball.score>=10){
                    flashcolor = SineColorMix(flashcolor,Color.green,timer,2);
                }
                if (ball.score>=15){
                    flashcolor = SineColorMix(Color.red,Color.blue,timer,2);
                    Color flashcolor2 = SineColorMix(Color.green,Color.blue,timer,2);
                    flashcolor = SineColorMix(flashcolor,flashcolor2,timer,1);
                }
                tM.color = SineColorMix(flashcolor,baseColor,timer,4);
            }
            else{
                //tM.color = rainbow.GetPixelBilinear((timer/100f)%1f,0.5f);
            }
        }
        else{
            tM.color = baseColor;
            timer = 180;
            //print(math.sin(math.PI/2+4*math.radians(timer)));
        }
    }
}
