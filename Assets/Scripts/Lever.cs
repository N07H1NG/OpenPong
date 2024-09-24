using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Lever : MonoBehaviour
{
    [SerializeField] GameObject myControlGroupObj;
    Controlgroup myControlGroup;
    // Start is called before the first frame update
    void Start()
    {
        myControlGroup = myControlGroupObj.GetComponent<Controlgroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<BallBehaviour>(out BallBehaviour plrComponent)){
            myControlGroup.GroupChangeState();
        }    
    }

    public void ReceiveChange(bool state){
        if (state){
            transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        }
        else{
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }

    }


}
