using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blitter : MonoBehaviour
{
    [SerializeField] Material postProcessMaterial;
    // Start is called before the first frame update
    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, postProcessMaterial);
        
    }
}
