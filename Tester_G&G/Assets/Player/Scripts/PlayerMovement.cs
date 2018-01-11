using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
        Rigidbody body = GetComponent<Rigidbody>();
        
        foreach(Touch touch in Input.touches)
        {
            if(touch.position.x < Screen.width/2)
            {
                body.AddForce(Vector3.left * 2);
            }
            else
            {
                body.AddForce(Vector3.right * 2);
            }

            if (SwipeManager.Instance.IsSwiping(SwipeDirection.Up))
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * 460);
            }
        }
        
        //if(Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width/2)
        //{
         //   body.AddForce(Vector3.left *2);
        //}
        //else if(Input.GetMouseButton(0))
        //{
         //   body.AddForce(Vector3.right *2);
        //}
        
        

    

}
}
