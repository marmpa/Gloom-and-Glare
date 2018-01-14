using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FollowCamera : MonoBehaviour {

    public TextMeshPro timerText;
    float seconds = 0f;
    float minutes = 0f;
    float newSeconds = 0f;
    void Start()
    {
        newSeconds = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {

        seconds = Time.realtimeSinceStartup - newSeconds;
        if (seconds > 60)
        {
            seconds = 0;
            newSeconds = Time.realtimeSinceStartup;
            minutes++;
        }


        timerText.SetText("Timer-> 0" + (int)minutes + ":" + (int)seconds);

    }
}
