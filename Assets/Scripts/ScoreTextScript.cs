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
    // Start is called before the first frame update
    void Start()
    {
        ball = PlayerObject.GetComponent<BallBehaviour>();
        tM = gameObject.GetComponent<TextMeshProUGUI>();
        baseColor = tM.color;
    }

    Color SineColorMix(Color col1,Color col2,float var){
        return col1*(1+math.sin(math.radians(var)))*0.5f + col2*(1-math.sin(math.radians(var)))*0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ball.infinity){
            tM.text = ball.score.ToString();
        }
        else{
            tM.text = "∞";
        }
        if (ball.score >=5){
            Color flashcolor = Color.red;
            if (ball.score>=10 ){
                flashcolor = SineColorMix(flashcolor,Color.green,timer*2);
            }
            if (ball.score>=15){
                flashcolor = SineColorMix(flashcolor,Color.blue,timer*4);
            }
            timer += Time.deltaTime*360;
            timer %= 360;
            tM.color = SineColorMix(baseColor,flashcolor,timer);
        }
        else{
            tM.color = baseColor;
        }
    }
}
