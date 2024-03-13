using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Exhibition : MonoBehaviour
{
    public Image flagPlayer;
    public Image starPlayer;

    public Image flagAI;
    public Image starAI;

    public TextMeshProUGUI txtValuePlayer;
    public TextMeshProUGUI namePlayer;

    public TextMeshProUGUI txtValueAI;
    public TextMeshProUGUI nameAI;

    private void Update()
    {
        // Set UI Player 
        flagPlayer.sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        namePlayer.text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        txtValuePlayer.text = PlayerPrefs.GetInt("valuePlayer", 1) + "/32";
        GetStarPlayer();

        // Set UI AI
        flagAI.sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
        nameAI.text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
        txtValueAI.text = PlayerPrefs.GetInt("valueAI", 1) + "/32";
        GetStarAI();
        
    }
    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }
    public void NextButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void ButtonLeftPlayer()
    {
        if(PlayerPrefs.GetInt("valuePlayer", 1) <= 1)
        {
            PlayerPrefs.SetInt("valuePlayer", 32); // there are 32 teams
        }
        else
        {
            int valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);
            valuePlayer--;
            PlayerPrefs.SetInt("valuePlayer",valuePlayer);
        }
    }
    public void ButtonRightPlayer()
    {
        if (PlayerPrefs.GetInt("valuePlayer", 1) >= 32)
        {
            PlayerPrefs.SetInt("valuePlayer", 1); 
        }
        else
        {
            int valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);
            valuePlayer++;
            PlayerPrefs.SetInt("valuePlayer", valuePlayer);
        }
    }
    public void ButtonLeftAI()
    {
        if (PlayerPrefs.GetInt("valueAI", 1) <= 1)
        {
            PlayerPrefs.SetInt("valueAI", 32); // there are 32 teams
        }
        else
        {
            int valueAI = PlayerPrefs.GetInt("valueAI", 1);
            valueAI--;
            PlayerPrefs.SetInt("valueAI", valueAI);
        }
    }
    public void ButtonRightAI()
    {
        if (PlayerPrefs.GetInt("valueAI", 1) >= 32)
        {
            PlayerPrefs.SetInt("valueAI", 1);
        }
        else
        {
            int valueAI = PlayerPrefs.GetInt("valueAI", 1);
            valueAI++;
            PlayerPrefs.SetInt("valueAI", valueAI);
        }
    }

    public void GetStarPlayer()
    {
        int valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);

        if(valuePlayer >= 1 && valuePlayer <= 8)
        {
            starPlayer.sprite = UITeam.Instance.star[0];
        }
        else if (valuePlayer >= 9 && valuePlayer <= 17)
        {
            starPlayer.sprite = UITeam.Instance.star[1];
        }
        else if (valuePlayer >= 18 && valuePlayer <= 25)
        {
            starPlayer.sprite = UITeam.Instance.star[2];
        }
        else
        {
            starPlayer.sprite = UITeam.Instance.star[3];
        }
        
    }

    public void GetStarAI()
    {
        int valueAI = PlayerPrefs.GetInt("valueAI", 1);

        if (valueAI >= 1 && valueAI <= 8)
        {
            starAI.sprite = UITeam.Instance.star[0];
        }
        else if (valueAI >= 9 && valueAI <= 17)
        {
            starAI.sprite = UITeam.Instance.star[1];
        }
        else if (valueAI >= 18 && valueAI <= 25)
        {
            starAI.sprite = UITeam.Instance.star[2];
        }
        else
        {
            starAI.sprite = UITeam.Instance.star[3];
        }

    }
}
