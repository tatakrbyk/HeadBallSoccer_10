using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Image flagLeft;
    public Image flagRight;

    public TextMeshProUGUI nameLeft;
    public TextMeshProUGUI nameRight;
    
    public TextMeshProUGUI result;
    public TextMeshProUGUI score;

    public int[,] valueTeamWC = new int[4,8];

    public GameObject pn_exhibition;
    public GameObject pn_WC;





    void Start()
    {
        flagLeft.sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        nameLeft.text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];

        flagRight.sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
        nameRight.text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];

        score.text = GameController.numberGoalsLeft + "-" + GameController.numberGoalsRight;
        if(GameController.numberGoalsLeft > GameController.numberGoalsRight)
        {
            result.text = "YOU WIN !";
        }
        else if (GameController.numberGoalsLeft == GameController.numberGoalsRight)
        {
            result.text = "Draw";
        }
        else
        {
            result.text = "YOU LOSE  !";
        }

        if(Menu.mode == (int)Menu.MODE.WORLDCUP)
        {
            pn_WC.SetActive(true);
            pn_exhibition.SetActive(false);
            SetupScorePlayerGroupStage();

        }
        else
        {
            pn_WC.SetActive(false);
            pn_exhibition.SetActive(true);
        }
    }

    public void ButtonHome()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonContinue()
    {
        SceneManager.LoadScene("WC");
    }
    public void ButtonRematch()
    {
        SceneManager.LoadScene("Game");
    }
    public void ButtonExhibition()
    {
        SceneManager.LoadScene("Exhibition");
    }


    public void SetupScorePlayerGroupStage()
    {
        int _scorePlayer = PlayerPrefs.GetInt("scoreTeam" + (PlayerPrefs.GetInt("valuePlayer", 1) - 1), 3);
        int _scoreAI = PlayerPrefs.GetInt("scoreTeam" + (PlayerPrefs.GetInt("valueAI", 1) - 1), 3);

        if (GameController.numberGoalsLeft < GameController.numberGoalsRight)
        {
            PlayerPrefs.SetInt("scoreTeam" + (PlayerPrefs.GetInt("valuePlayer", 1) - 1), _scorePlayer + 0);
            PlayerPrefs.SetInt("scoreTeam" + (PlayerPrefs.GetInt("valueAI", 1)-1), _scoreAI + 3);
        }
        else if (GameController.numberGoalsLeft == GameController.numberGoalsRight)
        {
            PlayerPrefs.SetInt("scoreTeam" + (PlayerPrefs.GetInt("valuePlayer", 1)-1), _scorePlayer + 1);
            PlayerPrefs.SetInt("scoreTeam" + (PlayerPrefs.GetInt("valueAI", 1)-1), _scoreAI + 1);
        }
        else 
        {
            PlayerPrefs.SetInt("scoreTeam" + (PlayerPrefs.GetInt("valuePlayer", 1)-1), _scorePlayer + 3);
            PlayerPrefs.SetInt("scoreTeam" + (PlayerPrefs.GetInt("valueAI", 1)-1), _scoreAI + 0);
        }
        if(PlayerPrefs.GetInt("matchStageWC", 0 ) <= 3)
        {
            ListSortScore();
        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 4)
        {
            PlayerPrefs.SetInt("score_R16" + PlayerPrefs.GetInt("valuePlayer", 1), GameController.numberGoalsLeft);
            PlayerPrefs.SetInt("score_R16" + PlayerPrefs.GetInt("valueAI", 1), GameController.numberGoalsRight);

        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 5)
        {
            PlayerPrefs.SetInt("score_R8_1" + PlayerPrefs.GetInt("valuePlayer", 1), GameController.numberGoalsLeft);
            PlayerPrefs.SetInt("score_R8_1" + PlayerPrefs.GetInt("valueAI", 1), GameController.numberGoalsRight);

        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 6)
        {
            PlayerPrefs.SetInt("score_R8_2" + PlayerPrefs.GetInt("valuePlayer", 1), GameController.numberGoalsLeft);
            PlayerPrefs.SetInt("score_R8_2" + PlayerPrefs.GetInt("valueAI", 1), GameController.numberGoalsRight);

        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 7)
        {
            PlayerPrefs.SetInt("score_R4_1" + PlayerPrefs.GetInt("valuePlayer", 1), GameController.numberGoalsLeft);
            PlayerPrefs.SetInt("score_R4_1" + PlayerPrefs.GetInt("valueAI", 1), GameController.numberGoalsRight);

        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 8)
        {
            PlayerPrefs.SetInt("score_R4_2" + PlayerPrefs.GetInt("valuePlayer", 1), GameController.numberGoalsLeft);
            PlayerPrefs.SetInt("score_R4_2" + PlayerPrefs.GetInt("valueAI", 1), GameController.numberGoalsRight);

        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 9)
        {
            PlayerPrefs.SetInt("score_r2" + PlayerPrefs.GetInt("valuePlayer", 1), GameController.numberGoalsLeft);
            PlayerPrefs.SetInt("score_r2" + PlayerPrefs.GetInt("valueAI", 1), GameController.numberGoalsRight);

        }
    }

    public void ListSortScore()
    {
        for(int i = 0; i < 8 ; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for(int k = j + 1; k < 4; k++)
                {
                    if(PlayerPrefs.GetInt("scoreTeam" + (valueTeamWC[j, i] - 1)) < PlayerPrefs.GetInt("scoreTeam" + (valueTeamWC[k,i] - 1)))
                    {
                        int temp2 = valueTeamWC[j, i];
                        valueTeamWC[j, i] = valueTeamWC[k, i];
                        valueTeamWC[k, i] = temp2;
                    }
                }
            }
        }

        for(int i = 0; i < 4 ; i++)
        {
            for( int j = 0; j < 8 ; j++)
            {
                PlayerPrefs.SetInt("valueTeamWC:" + i + "," + j, valueTeamWC[i, j]);
            }
        }
        Debug.Log("ListScore");
    }
}
