using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{

    // Use this for initialization
    Rigidbody body;
    private Vector3 forceTouch;
    private Vector3 forceSwipe;
    private int jumpForce;
    private GameObject feetObject;
    public bool fullR;

    private float startLocationY;
    void Start()
    {
        body = GetComponent<Rigidbody>();

        jumpForce = PlayerPrefs.GetInt("JumpHeight",430);
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
        
        

        VoidDeath();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        body.AddForce(forceTouch);
        
    }

    void VoidDeath()
    {
        //Debug.Log(transform.localPosition.y);
        if(transform.localPosition.y < -80)
        {
            //Debug.Log(SceneManager.GetActiveScene().buildIndex+" gies");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if(startLocationY-body.position.y>5)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    void OnCollisionExit(Collision collision)
    {
        startLocationY = body.position.y;
    }
}