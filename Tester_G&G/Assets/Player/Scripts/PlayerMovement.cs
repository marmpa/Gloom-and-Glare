using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // Use this for initialization
    Rigidbody body;
    private Vector3 forceTouch;
    private Vector3 forceSwipe;
    private int jumpForce;
    public bool fullR;
    void Start()
    {
        body = GetComponent<Rigidbody>();
        jumpForce = PlayerPrefs.GetInt("JumpHeight");
    }

    void Update()
    {
        forceTouch = Vector3.zero;
        forceSwipe = Vector3.zero;
        foreach (Touch touch in Input.touches)
        {
            if (touch.position.x < Screen.width / 2)
            {
                forceTouch = (Vector3.left * 2);
            }
            else
            {
                forceTouch = (Vector3.right * 2);
            }

        }

        if (SwipeManager.Instance.IsSwiping(SwipeDirection.Up))
        {
            forceSwipe = (Vector3.up * jumpForce);
        }

        if (Input.GetMouseButton(0) && Input.mousePosition.x < Screen.width / 2)
        {
            forceTouch = (Vector3.left * 2);
        }
        else if (Input.GetMouseButton(0))
        {
            forceTouch = (Vector3.right * 2);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (forceSwipe != Vector3.zero) { 
            body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z); }
        body.AddForce(forceTouch);
        body.AddForce(forceSwipe);
    }
}