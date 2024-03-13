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
            result.text = "YOU LOSE !";
        }
    }

    public void ButtonHome()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonRematch()
    {
        SceneManager.LoadScene("Game");
    }
    public void ButtonExhibition()
    {
        SceneManager.LoadScene("Exhibition");
    }

}
