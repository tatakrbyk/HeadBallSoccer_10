using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class TimeGift : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public Image getNowImg;
    public Button DailyGiftButton;

    public string _mm, _ss;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("PanelReward").GetComponent<RectTransform>().localScale = new Vector3(0,0,0);

        if(PlayerPrefs.GetInt("Start1" , 0) == 0)
        {
            timeText.enabled = false;
            getNowImg.enabled = true;
            DailyGiftButton.interactable = true;
        }   

    }
    private void Update()
    {
        if(PlayerPrefs.GetInt("Start1", 0) != 0)
            GetTimeGift();
    }

    public void GetTimeGift()
    {
        if(PlayerPrefs.GetInt("haskGetTimeGift", 0)  == 0 && PlayerPrefs.GetInt("Start1", 0) != 0)
        {
            PlayerPrefs.SetString("timeGiftStart", DateTime.Now.ToString());
            PlayerPrefs.SetInt("haskGetTimeGift", 1);
        }

        TimeSpan subTime = DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("timeGiftStart"));

        if (59 - subTime.Minutes >= 10)
        {
            _mm = (59 - subTime.Minutes).ToString();
        }
        else
        {
            _mm = "0" + (59 - subTime.Minutes);

        }

        if (59 - subTime.Seconds >= 10)
        {
            _ss = (59 - subTime.Seconds).ToString();
        }
        else
        {
            _ss = "0" + (59 - subTime.Seconds);

        }
        timeText.text = "0" + (5 - subTime.Hours) + ":" + _mm + ":" + _ss;

        if(subTime.TotalHours >= 6)
        {
            timeText.enabled = false;
            getNowImg.enabled = true;
            DailyGiftButton.interactable = true;
        }
        else
        {
            timeText.enabled = true;
            getNowImg.enabled = false;
            DailyGiftButton.interactable = false;
        }
    }

    public void ButtonDailyGift()
    {
        // UnityAds.Instance.ShowAd();
    }

    public void ButtonGetReward()
    {
        int _money = PlayerPrefs.GetInt("money");
        _money += 5000;
        PlayerPrefs.SetInt("money", _money);
        PlayerPrefs.SetInt("haskGetTimeGift", 0);

        GameObject.FindGameObjectWithTag("PanelReward").GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        PlayerPrefs.SetInt("Start1", 1);
    }
}
