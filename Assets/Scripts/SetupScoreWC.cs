using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScoreWC : MonoBehaviour
{
    public static SetupScoreWC instance;

    private void Awake()
    {
        instance = this;
    }
    public void SetupScoreGroupStage()
    {
        for (int i = 0; i < WorldCupController.instance.listGroup1.Count; i++) 
        {
            int _scoreTeam1 = PlayerPrefs.GetInt("scoreTeam" + (WorldCupController.instance.listGroup1[i] - 1), 3);
            int _scoreTeam2 = PlayerPrefs.GetInt("scoreTeam" + (WorldCupController.instance.listGroup2[i] - 1), 3);

            int random = Random.Range(0, 3);
            if(random == 0)
            {
                PlayerPrefs.SetInt("scoreTeam" + (WorldCupController.instance.listGroup1[i] - 1), _scoreTeam1+ 3);
                PlayerPrefs.SetInt("scoreTeam" + (WorldCupController.instance.listGroup2[i] - 1), _scoreTeam2 + 0);
            }
            if (random == 1)
            {
                PlayerPrefs.SetInt("scoreTeam" + (WorldCupController.instance.listGroup1[i] - 1), _scoreTeam1 + 1);
                PlayerPrefs.SetInt("scoreTeam" + (WorldCupController.instance.listGroup2[i] - 1), _scoreTeam2 + 1);
            }
            else
            {
                PlayerPrefs.SetInt("scoreTeam" + (WorldCupController.instance.listGroup2[i] - 1), _scoreTeam1 + 0);
                PlayerPrefs.SetInt("scoreTeam" + (WorldCupController.instance.listGroup1[i] - 1), _scoreTeam2 + 3);

            }
        }
    }

    public void SetupScoreR16()
    {
        for(int i = 0; i < WorldCupController.instance.listR16_1.Count; i++)
        {
            int random1 = Random.Range(0, 6);
            int random2 = Random.Range(5, 10);

            PlayerPrefs.SetInt("score_R16" + (WorldCupController.instance.listR16_1[i]), random1);
            PlayerPrefs.SetInt("score_R16" + (WorldCupController.instance.listR16_2[i]), random2);
        }
    }
}
