using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldCupController : MonoBehaviour
{
    public static WorldCupController instance;
    public TextMeshProUGUI nameTeamTextWC;
    public TextMeshProUGUI ÝDTeamTextWC;
    public Image flagTeamWC;
    public Image starTeamWC;

    public GameObject panelSelectTeam;
    public GameObject panelGroupStage;
    public GameObject panelR16;
    public int[,] valueTeamWC = new int[4,8];

    public int[] scoreTeam = new int[46];

    public List<int> listGroup1 = new List<int>();
    public List<int> listGroup2 = new List<int>();
    
    public List<int> listR16_1 = new List<int>();
    public List<int> listR16_2 = new List<int>();


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if(PlayerPrefs.GetInt("selectTeamWC",0)  == 0)
        {
            panelSelectTeam.SetActive(true);
            panelGroupStage.SetActive(false);
            panelR16.SetActive(false);

            for(int i = 0; i < 32; i++)
            {
                PlayerPrefs.SetInt("scoreTeam" + i, 0);
            }
        }
        else
        {
            if(PlayerPrefs.GetInt("matchStageWC", 0) <= 3)
            {
                panelSelectTeam.SetActive(false);
                panelR16.SetActive(false);
                panelGroupStage.SetActive(true);

                GetUIStageGroup();
                SetupMatchGroupStage();
                SetupR16();
            }
            else if(PlayerPrefs.GetInt("matchStageWC", 0) == 4)
            {
                panelSelectTeam.SetActive(false);
                panelGroupStage.SetActive(false);
                panelR16.SetActive(true);

                SetupR16();
            }
            
        }
    }
    private void Update()
    {
        flagTeamWC.sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        nameTeamTextWC.text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        ÝDTeamTextWC.text = PlayerPrefs.GetInt("valuePlayer", 1) + "/32";
        GetStarWC();
        

    }
    public void ButtonLeftSelectTeamWC()
    {
        if (PlayerPrefs.GetInt("valuePlayer", 1) <= 1)
        {
            PlayerPrefs.SetInt("valuePlayer", 32); // there are 32 teams
        }
        else
        {
            int valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);
            valuePlayer--;
            PlayerPrefs.SetInt("valuePlayer", valuePlayer);
        }
    }
    public void ButtonRightSelectTeamWC()
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
        SetupGroupStage();
    }
    public void GetStarWC()
    {
        int valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);

        if (valuePlayer >= 1 && valuePlayer <= 8)
        {
            starTeamWC.sprite = UITeam.Instance.star[0];
        }
        else if (valuePlayer >= 9 && valuePlayer <= 17)
        {
            starTeamWC.sprite = UITeam.Instance.star[1];
        }
        else if (valuePlayer >= 18 && valuePlayer <= 25)
        {
            starTeamWC.sprite = UITeam.Instance.star[2];
        }
        else
        {
            starTeamWC.sprite = UITeam.Instance.star[3];
        }

    }

    public void ButtonBack()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonNextPanelSelectTeam()
    {
        panelGroupStage.SetActive(true);
        panelSelectTeam.SetActive(false);
        PlayerPrefs.SetInt("selectTeamWC", 1);
        SetupGroupStage();
    }

    public void ButtonNextR16()
    {
        if (PlayerPrefs.GetInt("matchStageWC", 0) == 3)
        {      
            SceneManager.LoadScene("Game"); 
        }
        
    }
    public void ButtonNextGroupStage()
    {
        if (PlayerPrefs.GetInt("matchStageWC", 0) < 3)
        {      
            SceneManager.LoadScene("Game"); 
        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) >= 3)
        {
            panelSelectTeam.SetActive(false);
            panelGroupStage.SetActive(false);
            panelR16.SetActive(true);

            SetupR16();
        }
        
    }

    public void SetupGroupStage()
    {
        List<int> _listTeam = new List<int>();
        for (int i = 1; i < 33; i++)
        {
            _listTeam.Add(i);
        }

        for(int i = 0; i < 4 ; i++)
        {
            for(int j = 0; j < 8 ; j++)
            {
                int _rd = Random.Range(0, _listTeam.Count);

                valueTeamWC[i,j] = _listTeam[_rd];
                _listTeam.RemoveAt(_rd);
                PlayerPrefs.SetInt("valueTeamWC:" + i + "," + j, valueTeamWC[i, j]);
                Debug.Log(PlayerPrefs.GetInt("valueTeamWC:" + i + "," + j));
            }
        }

        for(int i = 0; i <8 ; i++)
        {
            PlayerPrefs.SetInt("list" + 0 + "," + i, valueTeamWC[0, i]);
            PlayerPrefs.SetInt("list" + 1 + "," + i, valueTeamWC[1, i]);
            PlayerPrefs.SetInt("list" + 2 + "," + i, valueTeamWC[2, i]);
            PlayerPrefs.SetInt("list" + 3 + "," + i, valueTeamWC[3, i]);
        }
        SceneManager.LoadScene("WC");
    }

    public void GetUIStageGroup()
    {
        GameObject[] _uiTeam = GameObject.FindGameObjectsWithTag("GroupStage");
        for(int i = 0; i < _uiTeam.Length; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                _uiTeam[i].GetComponent<UIGroupStage>().flagTeam[j].sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valueTeamWC:" + j + "," + i) - 1];
                _uiTeam[i].GetComponent<UIGroupStage>().nameTeamText[j].text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valueTeamWC:" + j + "," + i) - 1];
                _uiTeam[i].GetComponent<UIGroupStage>().pointText[j].text = PlayerPrefs.GetInt("scoreTeam" + (PlayerPrefs.GetInt("valueTeamWC:" + j + "," + i, 0)-1)).ToString();

                if (PlayerPrefs.GetInt("valueTeamWC:" + j + "," + i) == PlayerPrefs.GetInt("valuePlayer", 1))
                {
                    _uiTeam[i].GetComponent<UIGroupStage>().nameTeamText[j].color = Color.green;
                    _uiTeam[i].GetComponent<UIGroupStage>().pointText[j].color = Color.green;
                }
            }
        }

        
    }

    public void SetupR16()
    {
        int[,] _valueR16 = new int[2, 8];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 8 ; j++)
            {
                if(i == 0 )
                {
                    _valueR16[0, j] = PlayerPrefs.GetInt("valueTeamWC:" + i + "," + j, 0);
                }
                else
                {
                    _valueR16[1, 0] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 1, 0);
                    _valueR16[1, 1] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 0, 0);

                    _valueR16[1, 2] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 3, 0);
                    _valueR16[1, 3] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 2, 0);

                    _valueR16[1, 4] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 5, 0);
                    _valueR16[1, 5] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 4, 0);

                    _valueR16[1, 6] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 7, 0);
                    _valueR16[1, 7] = PlayerPrefs.GetInt("valueTeamWC:" + 1 + "," + 6, 0);

                }
            }
        }
        
        for(int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                PlayerPrefs.SetInt("value_r16:" + i + "," + j, _valueR16[i,j]);
            }
        }

        GameObject[] uiTeam = GameObject.FindGameObjectsWithTag("R16");

        for(int i = 0; i < uiTeam.Length;i++)
        {
            
            string _parent = uiTeam[i].name;
            for (int j = 0; j < 2 ; j++)
            {
                uiTeam[i].transform.GetChild(j).GetComponent<UIR16>().flag.sprite =
                       UITeam.Instance.flagTeam[PlayerPrefs.GetInt("value_r16:" + j + "," + i) - 1];

                uiTeam[i].transform.GetChild(j).GetComponent<UIR16>().nameText.text =
                    UITeam.Instance.nameTeam[PlayerPrefs.GetInt("value_r16:" + j + "," + i) - 1];
                
                if (PlayerPrefs.GetInt("matchStageWC", 0) == 3)
                {
                    uiTeam[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text = "-";

                }
                else
                {
                    uiTeam[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text = PlayerPrefs.GetInt("score_R16" + PlayerPrefs.GetInt("value_r16:" + j + "," + i)).ToString()  ;
                    ;

                }
                if (PlayerPrefs.GetInt("valuePlayer", 1) == PlayerPrefs.GetInt("value_r16:" + j + "," + i))
                {
                    uiTeam[i].transform.GetChild(j).GetComponent<UIR16>().nameText.color = Color.green;
                    uiTeam[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.color = Color.green;
                }
            }
            
        }
    }
    public void SetupMatchGroupStage()
    {
        int match = PlayerPrefs.GetInt("matchStageWC", 0);
        switch (match)
        {
            case 0:
                listGroup1.Clear();
                listGroup2.Clear();
                for (int i = 0; i < 8; i++)
                {
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 0 + "," + i));
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 2 + "," + i));
                }
                for (int i = 0; i < 8; i++)
                {
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 1 + "," + i));
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 3 + "," + i));
                }

                for (int i = 0; i < listGroup1.Count; i++)
                {
                    if (listGroup1[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listGroup2[i]);
                        listGroup1.RemoveAt(i);
                        listGroup2.RemoveAt(i);
                    }
                    else if (listGroup2[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listGroup1[i]);
                        listGroup1.RemoveAt(i);
                        listGroup2.RemoveAt(i);
                    }
                }
                Debug.Log("teamAI + team Player " + PlayerPrefs.GetInt("valueAI") + " - " + PlayerPrefs.GetInt("valuePlayer"));
                break;

            case 1:
                listGroup1.Clear();
                listGroup2.Clear();
                for (int i = 0; i < 8; i++)
                {
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 0 + "," + i));
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 1 + "," + i));
                }
                for (int i = 0; i < 8; i++)
                {
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 2 + "," + i));
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 3 + "," + i));
                }

                for (int i = 0; i < listGroup1.Count; i++)
                {
                    if (listGroup1[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listGroup2[i]);
                        listGroup1.RemoveAt(i);
                        listGroup2.RemoveAt(i);
                    }
                    else if (listGroup2[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listGroup1[i]);
                        listGroup1.RemoveAt(i);
                        listGroup2.RemoveAt(i);
                    }
                }
                Debug.Log("teamAI + team Player " + PlayerPrefs.GetInt("valueAI") + " - " + PlayerPrefs.GetInt("valuePlayer"));
                break;
            case 2:
                listGroup1.Clear();
                listGroup2.Clear();
                for (int i = 0; i < 8; i++)
                {
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 0 + "," + i));
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 3 + "," + i));
                }
                for (int i = 0; i < 8; i++)
                {
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 1 + "," + i));
                    listGroup1.Add(PlayerPrefs.GetInt("list" + 2 + "," + i));
                }

                for (int i = 0; i < listGroup1.Count; i++)
                {
                    if (listGroup1[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listGroup2[i]);
                        listGroup1.RemoveAt(i);
                        listGroup2.RemoveAt(i);
                    }
                    else if (listGroup2[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listGroup1[i]);
                        listGroup1.RemoveAt(i);
                        listGroup2.RemoveAt(i);
                    }
                }
                Debug.Log("teamAI + team Player " + PlayerPrefs.GetInt("valueAI") + " - " + PlayerPrefs.GetInt("valuePlayer"));
                break;
            case 3:
                listR16_1.Clear();
                listR16_2.Clear();

                for(int i = 0; i < 8; i++)
                {
                    listR16_1.Add(PlayerPrefs.GetInt("value_r16:" + 0 + "," + i));
                    listR16_2.Add(PlayerPrefs.GetInt("value_r16:" + 1 + "," + i));
                }

                for (int i = 0; i < listR16_1.Count; i++)
                {
                    if (listR16_1[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listR16_2[i]);
                        listR16_1.RemoveAt(i);
                        listR16_2.RemoveAt(i);
                    }
                    if (listR16_2[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listR16_1[i]);
                        listR16_1.RemoveAt(i);
                        listR16_2.RemoveAt(i);
                    }
                }

                    break;
            
        }
    }
}
