using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGBehaviour : MonoBehaviour
{
    MeshRenderer myMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        myMeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myMeshRenderer.material.SetColor("_Colour",Color.cyan);
    }
}
