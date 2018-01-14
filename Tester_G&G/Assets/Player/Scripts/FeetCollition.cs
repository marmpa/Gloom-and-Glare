using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeetCollition : MonoBehaviour {

    public GameObject player; 
    float seconds = 0f;
    float minutes = 0f;
    float newSeconds = 0f;

    private Vector3 forceSwipe;
    private int jumpForce;
    private Rigidbody body;

    private bool canJump;


    private Vector3 offset;

    void Awake()
    {
        //FB.Init(SetInit, OnHideUnity);
        
        
        
        

    }

    void FixedUpdate()
    {
        if (canJump)
        {
            if (forceSwipe != Vector3.zero)
            {

                body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
            }

            body.AddForce(forceSwipe);

        }
        
    }

    void Update()
    {
        forceSwipe = Vector3.zero;
        if (SwipeManager.Instance.IsSwiping(SwipeDirection.Up))
        {
            forceSwipe = (Vector3.up * jumpForce);
        }
    }
    void Start ()
    {
        offset = transform.position - player.transform.position;
        newSeconds = Time.realtimeSinceStartup;
        jumpForce = PlayerPrefs.GetInt("JumpHeight", 430);
        body = player.GetComponent<Rigidbody>();
        canJump = true;
    }
    

    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("Fb is logged in");
            }
            else
            {
                Debug.Log("FB is not logged in");
            }
            
        }

    }

    void SetInit()
    {
       if(!FB.IsLoggedIn)
        {
            List<string> permissions = new List<string>();
            
            permissions.Add("publish_actions");
            
            FB.LogInWithPublishPermissions(permissions,AuthCallBack);
        }
    }

    public void SetScore(string score)
    {
        var scoreData = new Dictionary<string, string>();
        scoreData["score"] = score;
        Debug.Log("to score einai " + scoreData["score"]);
        FB.API("/me/scores", HttpMethod.POST, ScoreDCallBack, scoreData);
    }



    public void ScoreDCallBack(IResult result)
    {
       // Debug.Log(result.ResultDictionary["score"]+"nai leitourge!!!!!!!!!!!!!!!!!!!!OOOOOOOOOOOOOOOOOOOOO");
    }

    void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }




    void LateUpdate ()
    {
       transform.position = player.transform.position + offset;
    }

    void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.CompareTag("Untagged") && !canJump)
        {
            canJump = true;
            Debug.Log(collider.gameObject.tag);
        }

            if (collider.gameObject.CompareTag("end"))
        {
            if(PlayerPrefs.GetInt("levelReached")<=SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("levelReached", SceneManager.GetActiveScene().buildIndex+1);
            }

            seconds = Time.realtimeSinceStartup - newSeconds;
            if (seconds > 60)
            {
                seconds = 0;
                newSeconds = Time.realtimeSinceStartup;
                minutes++;
            }

            string score = (int)minutes + ":" + (int)seconds;
            //if(FB.IsLoggedIn)
            //{
            //    SetScore(score);
            //}
            

            SceneManager.LoadScene("Menu");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Untagged") && canJump)
        {
           
            canJump = false;
        }
        
    }


}
