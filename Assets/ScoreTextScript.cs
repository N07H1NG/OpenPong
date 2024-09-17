using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextScript : MonoBehaviour
{
    [SerializeField] GameObject PlayerObject;
    BallBehaviour ball;
    TextMeshProUGUI tM;
    // Start is called before the first frame update
    void Start()
    {
        ball = PlayerObject.GetComponent<BallBehaviour>();
        tM = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tM.text = ball.score.ToString();
    }
}
