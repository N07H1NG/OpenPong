using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float EaseInOutQuadratic(float alpha)
    {
        if (alpha <= 0.5f)
        {
            return 2*alpha*alpha;
        }
        alpha -= 0.5f;
        return 2 * alpha * (1-alpha) + 0.5f;
    }

    public static float EaseInOutBesier(float alpha)
    {
        return alpha * alpha * (3.0f - 2.0f * alpha);
    }
}
