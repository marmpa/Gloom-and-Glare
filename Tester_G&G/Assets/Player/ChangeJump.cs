using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeJump : MonoBehaviour {

    public Slider jumpSlider;
    public GameObject sliderText;
    private TextMeshProUGUI text;

    

    void Start()
    {
        if (!PlayerPrefs.HasKey("JumpHeight"))
        {
            PlayerPrefs.SetInt("JumpHeight", 430);
        }
        //jumpSlider.onValueChanged.AddListener(delegate { OnChangedValue(); });
        text = sliderText.GetComponent<TextMeshProUGUI>();
        Debug.Log("on start"+ PlayerPrefs.GetInt("JumpHeight").ToString());
        text.SetText(PlayerPrefs.GetInt("JumpHeight").ToString());
        jumpSlider.value = PlayerPrefs.GetInt("JumpHeight")/2000.0f;
    }

    public void OnChangedValue()
    {
        int value = Mathf.RoundToInt(jumpSlider.value * 2000);
        if(value==2000)
        {
            value = PlayerPrefs.GetInt("JumpHeight");
        }

        if(value<10)
        {
            value = 430;
        }
        PlayerPrefs.SetInt("JumpHeight", value);
        text.SetText(value.ToString());


        Debug.Log(PlayerPrefs.GetInt("JumpHeight"));
        PlayerPrefs.Save();
    }

}
