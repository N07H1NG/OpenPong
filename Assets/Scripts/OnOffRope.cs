using System.Collections;
using System.Collections.Generic;
using SplineMesh;
using UnityEngine;

public class OnOffRope : OnOff
{
    [SerializeField] Material OnMat;
    [SerializeField] Material OffMat;

    // Start is called before the first frame update
    public override void ChangeState(bool state)
    {
        isItOn = state;
        SplineMeshTiling splinemesh = GetComponent<SplineMeshTiling>();
        splinemesh.material = isItOn ? OnMat : OffMat;
        splinemesh.CreateMeshes();
        
    }
}
