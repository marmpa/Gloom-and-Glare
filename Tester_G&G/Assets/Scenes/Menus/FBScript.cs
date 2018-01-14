using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;
using TMPro;

public class FBScript : MonoBehaviour {

    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilePic;
    public GameObject ScoreEntryPanel;
    public GameObject ScoreScrollList;

    private List<object> scoresList = null;

   

    private Dictionary<string, string> profile = null;

    void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }

    
    

    void SetInit()
    {
        if(FB.IsLoggedIn)
        {
            Debug.Log("logged in");
        }
        else
        {
            Debug.Log("Yes ");
        }

        DealWithFBMenus(FB.IsLoggedIn);
    }

    void OnHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        permissions.Add("email");
        permissions.Add("publish_actions");

        FB.LogInWithPublishPermissions(permissions, AuthCallBack);
        
    }

    public void FBLogout()
    {
        FB.LogOut();
        DealWithFBMenus(FB.IsLoggedIn);
    }

    public void QueryScores()
    {
        SetScore();
        FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, ScoresCallback);
    }

    private void ScoresCallback(IResult result)
    {
        Debug.Log("Scores callback: gie mes lene mario");
        


        scoresList = Util.DeserializeScores(result.RawResult);

        foreach (Transform child in ScoreScrollList.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (object score in scoresList)
        {

            var entry = (Dictionary<string, object>)score;
            var user = (Dictionary<string, object>)entry["user"];




            GameObject ScorePanel;
            ScorePanel = Instantiate(ScoreEntryPanel) as GameObject;
            ScorePanel.transform.parent = ScoreScrollList.transform;

            Transform ThisScoreName = ScorePanel.transform.Find("FriendName");
            Transform ThisScoreScore = ScorePanel.transform.Find("FriendScore");
            TextMeshProUGUI ScoreName = ThisScoreName.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI ScoreScore = ThisScoreScore.GetComponent<TextMeshProUGUI>();

            ScoreName.SetText( user["name"].ToString());
            ScoreScore.SetText(entry["score"].ToString());

            Transform TheUserAvatar = ScorePanel.transform.Find("FriendAvatar");
            Image UserAvatar = TheUserAvatar.GetComponent<Image>();


            FB.API(Util.GetPictureURL(user["id"].ToString(), 128, 128), HttpMethod.GET, delegate (IGraphResult pictureResult) {

                if (pictureResult.Error != null) // if there was an error
                {
                    Debug.Log(pictureResult.Error);
                }
                else // if everything was fine
                {
                    UserAvatar.sprite = Sprite.Create(pictureResult.Texture, new Rect(0, 0, 128, 128), new Vector2(0, 0));
                }

            });



        }


    }


    public void SetScore()
    {
        var scoreData = new Dictionary<string, string>();
        scoreData["score"] = UnityEngine.Random.Range(10, 200).ToString();
        FB.API("/me/scores", HttpMethod.POST, delegate(IGraphResult result) { Debug.Log("score submitted succesfully"); }, scoreData);
    }

    

    public void ScoreDCallBack(IGraphResult result)
    {

    }

    void AuthCallBack(IResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if(FB.IsLoggedIn)
            {
                Debug.Log("Fb is logged in");
            }
            else
            {
                Debug.Log("FB is not logged in");
            }

            DealWithFBMenus(FB.IsLoggedIn);
        }

    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if(isLoggedIn)
        {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);

            FB.API("/me?fields=first_name",HttpMethod.GET, DisplayUsername);
            FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET,DisplayProfilePic);
        }
        else
        {
            DialogLoggedIn.SetActive(false);
            DialogLoggedOut.SetActive(true);
        }
    }

    void DisplayUsername(IResult result)
    {
        TextMeshProUGUI username = DialogUsername.GetComponent<TextMeshProUGUI>();

        if(result.Error == null)
        {
            username.SetText(result.ResultDictionary["first_name"].ToString());
            
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    void DisplayProfilePic(IGraphResult result)
    {
        if(result.Texture != null)
        {
            Image profilePic = DialogProfilePic.GetComponent<Image>();

            profilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
  
        }
        
    }
}
