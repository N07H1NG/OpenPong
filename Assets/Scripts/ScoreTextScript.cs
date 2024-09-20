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

    // Update is called once per frame
    void Update()
    {
        tM.text = ball.score.ToString();
        if (ball.score >=10){
            timer += Time.deltaTime*360;
            timer %= 360;
            tM.color = baseColor*(1+math.sin(math.radians(timer)))*0.5f + Color.red*(1-math.sin(math.radians(timer)))*0.5f;
        }
        else{
            tM.color = baseColor;
        }
    }
}
