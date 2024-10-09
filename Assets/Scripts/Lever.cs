using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
            GetComponentInChildren<Animation>()["Scene"].speed = 1.0f;
            GetComponentInChildren<Animation>().Play();
            //transform.rotation = Quaternion.Euler(new Vector3(-15,0,-90));
        }
        else{
            if(!GetComponentInChildren<Animation>().IsPlaying("Scene")){
                GetComponentInChildren<Animation>().Play();
                GetComponentInChildren<Animation>()["Scene"].normalizedTime = 1;
            }
            GetComponentInChildren<Animation>()["Scene"].speed = -1;
            GetComponentInChildren<Animation>().Play();
            //transform.rotation = Quaternion.Euler(new Vector3(15,-180,-90));
        }

    }


}
