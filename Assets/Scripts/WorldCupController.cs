using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    public GameObject panelR8;
    public GameObject panelR4;
    public GameObject panelR2;
    

    public int[,] valueTeamWC = new int[4,8];

    public int[] scoreTeam = new int[46];

    public List<int> listGroup1 = new List<int>();
    public List<int> listGroup2 = new List<int>();
    
    public List<int> listR16_1 = new List<int>();
    public List<int> listR16_2 = new List<int>();
    
    public List<int> listR8_1 = new List<int>();
    public List<int> listR8_2 = new List<int>();

    public List<int> listR4_1 = new List<int>();
    public List<int> listR4_2 = new List<int>();

    public List<int> listR2 = new List<int>();


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
            panelR8.SetActive(false);
            panelR4.SetActive(false);
            panelR2.SetActive(false);
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
                panelR8.SetActive(false);
                panelR4.SetActive(false);
                panelR2.SetActive(false);
                panelGroupStage.SetActive(true);

                GetUIStageGroup();
                SetupMatchGroupStage();
                SetupR16();
            }
            else if(PlayerPrefs.GetInt("matchStageWC", 0) == 4)
            {
                panelSelectTeam.SetActive(false);
                panelGroupStage.SetActive(false);
                panelR8.SetActive(false);
                panelR4.SetActive(false);
                panelR2.SetActive(false);

                panelR16.SetActive(true);


                SetupR16();
                SetupMatchGroupStage();
            }
            else if(PlayerPrefs.GetInt("matchStageWC", 0) == 5 || PlayerPrefs.GetInt("matchStageWC", 0) == 6)
            {
                panelSelectTeam.SetActive(false);
                panelGroupStage.SetActive(false);
                panelR16.SetActive(false);
                panelR4.SetActive(false);
                panelR2.SetActive(false);

                panelR8.SetActive(true);


                SetupMatchGroupStage();
                SetupR8();
            }
            else if (PlayerPrefs.GetInt("matchStageWC", 0) == 7 || PlayerPrefs.GetInt("matchStageWC", 0) == 8)
            {
                panelSelectTeam.SetActive(false);
                panelGroupStage.SetActive(false);
                panelR16.SetActive(false);
                panelR8.SetActive(false);
                panelR2.SetActive(false);

                panelR4.SetActive(true);


                SetupMatchGroupStage();
                SetupR4();
            }
            else
            {
                panelSelectTeam.SetActive(false);
                panelGroupStage.SetActive(false);
                panelR16.SetActive(false);
                panelR8.SetActive(false);
                panelR4.SetActive(false);

                panelR2.SetActive(true);


                SetupMatchGroupStage();
                SetupR2();
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

    public void ButtonNextR2()
    {
        if (PlayerPrefs.GetInt("matchStageWC", 0) <= 9)
        {
            SceneManager.LoadScene("Game");
        }
        

    }
    public void ButtonNextR4()
    {
        if (PlayerPrefs.GetInt("matchStageWC", 0) <= 7)
        {
            SceneManager.LoadScene("Game");
        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 8)
        {
            panelR4.SetActive(false);
            panelR2.SetActive(true);

            SetupR2();
        }

    }
    public void ButtonNextR8()
    {
        if (PlayerPrefs.GetInt("matchStageWC", 0) <= 5)
        {
            SceneManager.LoadScene("Game");
        }
        else if (PlayerPrefs.GetInt("matchStageWC", 0) == 6)
        {
            panelR8.SetActive(false);
            panelR4.SetActive(true);
            
            SetupR4();
        }

    }

    public void ButtonNextR16()
    {
        if (PlayerPrefs.GetInt("matchStageWC", 0) == 3)
        {      
            SceneManager.LoadScene("Game"); 
        }
        else if( PlayerPrefs.GetInt("matchStageWC", 0) == 4)
        {
            panelR8.SetActive(true);
            panelR16.SetActive(false);

            SetupR8();
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

    public void SetupR2()
    {
        GameObject team = GameObject.FindGameObjectWithTag("R2");
        for(int i = 0;  i < 2; i++)
        {
            team.transform.GetChild(i).GetComponent<UIR16>().flag.sprite =
                        UITeam.Instance.flagTeam[PlayerPrefs.GetInt("value_r2::" + i) - 1];
            team.transform.GetChild(i).GetComponent<UIR16>().nameText.text =
                        UITeam.Instance.nameTeam[PlayerPrefs.GetInt("value_r2::" + i) - 1];

            if (PlayerPrefs.GetInt("matchStageWC", 0) == 8)
            {
                team.transform.GetChild(i).GetComponent<UIR16>().scoreText.text = "-";
            }
            else if(PlayerPrefs.GetInt("matchStageWC", 0) == 9)
            {
                team.transform.GetChild(i).GetComponent<UIR16>().scoreText.text =
                     PlayerPrefs.GetInt("score_r2 " + PlayerPrefs.GetInt("value_r2:" + i)).ToString();
            }
            if (PlayerPrefs.GetInt("valuePlayer", 1) == PlayerPrefs.GetInt("value_r2:" + i))
            {
                team.transform.GetChild(i).GetComponent<UIR16>().nameText.color = Color.green;
                team.transform.GetChild(i).GetComponent<UIR16>().scoreText.color = Color.green;
            }



        }
    }
    public void SetupR4()
    {
        GameObject[] _uiTeam_1 = GameObject.FindGameObjectsWithTag("R4_1");
        GameObject[] _uiTeam_2 = GameObject.FindGameObjectsWithTag("R4_2");

        for (int i = 0; i < _uiTeam_1.Length; i++)
        {

            for (int j = 0; j < 2; j++)
            {
                _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().flag.sprite =
                        UITeam.Instance.flagTeam[PlayerPrefs.GetInt("value_r4::" + j + "," + i) - 1];

                _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().nameText.text =
                    UITeam.Instance.nameTeam[PlayerPrefs.GetInt("value_r4:" + j + "," + i) - 1];

                if (PlayerPrefs.GetInt("matchStageWC", 0) == 6)
                {
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text = "-";
                }
                else
                {
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text =
                        PlayerPrefs.GetInt("score_R4_1" + PlayerPrefs.GetInt("value_r4:" + j + "," + i)).ToString();

                }
                if (PlayerPrefs.GetInt("valuePlayer", 1) == PlayerPrefs.GetInt("value_r4:" + j + "," + i))
                {
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().nameText.color = Color.green;
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.color = Color.green;
                }
            }
        }

        for (int i = 0; i < _uiTeam_2.Length; i++)
        {

            for (int j = 0; j < 2; j++)
            {
                _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().flag.sprite =
                        UITeam.Instance.flagTeam[PlayerPrefs.GetInt("value_r4::" + j + "," + i) - 1];

                _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().nameText.text =
                    UITeam.Instance.nameTeam[PlayerPrefs.GetInt("value_r4:" + j + "," + i) - 1];

                if (PlayerPrefs.GetInt("matchStageWC", 0) <= 7)
                {
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text = "-";
                }
                else
                {
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text =
                        PlayerPrefs.GetInt("score_R4_2 " + PlayerPrefs.GetInt("value_r4:" + j + "," + i)).ToString();

                }
                if (PlayerPrefs.GetInt("valuePlayer", 1) == PlayerPrefs.GetInt("value_r4:" + j + "," + i))
                {
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().nameText.color = Color.green;
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.color = Color.green;
                }
            }
        }
    }
    public void SetupR8()
    {
        GameObject[] _uiTeam_1 = GameObject.FindGameObjectsWithTag("R8_1");
        GameObject[] _uiTeam_2 = GameObject.FindGameObjectsWithTag("R8_2");

        for (int i = 0; i < _uiTeam_1.Length; i++)
        {

            for (int j = 0; j < 2; j++)
            {
                _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().flag.sprite =
                        UITeam.Instance.flagTeam[PlayerPrefs.GetInt("value_r8::" + j + "," + i) - 1];

                _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().nameText.text =
                    UITeam.Instance.nameTeam[PlayerPrefs.GetInt("value_r8:" + j + "," + i) - 1];

                if (PlayerPrefs.GetInt("matchStageWC", 0) == 4)
                {
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text = "-";
                }
                else
                {
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text =
                        PlayerPrefs.GetInt("score_R8_1" + PlayerPrefs.GetInt("value_r8:" + j + "," + i)).ToString();

                }
                if (PlayerPrefs.GetInt("valuePlayer", 1) == PlayerPrefs.GetInt("value_r8:" + j + "," + i))
                {
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().nameText.color = Color.green;
                    _uiTeam_1[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.color = Color.green;
                }
            }
        }


        for (int i = 0; i < _uiTeam_2.Length; i++)
        {

            for (int j = 0; j < 2; j++)
            {
                _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().flag.sprite =
                        UITeam.Instance.flagTeam[PlayerPrefs.GetInt("value_r8::" + j + "," + i) - 1];

                _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().nameText.text =
                    UITeam.Instance.nameTeam[PlayerPrefs.GetInt("value_r8:" + j + "," + i) - 1];

                if (PlayerPrefs.GetInt("matchStageWC", 0) <= 5)
                {
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text = "-";
                }
                else
                {
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.text = 
                        PlayerPrefs.GetInt("score_R8_2" + PlayerPrefs.GetInt("value_r8:" + j + "," + i)).ToString();

                }
                if (PlayerPrefs.GetInt("valuePlayer", 1) == PlayerPrefs.GetInt("value_r8:" + j + "," + i))
                {
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().nameText.color = Color.green;
                    _uiTeam_2[i].transform.GetChild(j).GetComponent<UIR16>().scoreText.color = Color.green;
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
            case 4:
            case 5:
                for(int i = 0; i < 8; i++)
                {
                    if (PlayerPrefs.GetInt("score_R16" + PlayerPrefs.GetInt("value_r16:" + 0 + "," + i)) >=
                        PlayerPrefs.GetInt("score_R16" + PlayerPrefs.GetInt("value_r16:" + 1 + "," + i)))
                    {
                        if( i % 2 == 0)
                        {
                            listR8_1.Add(PlayerPrefs.GetInt("value_r16:" + 0 + "," + i));
                        }
                        else
                        {
                            listR8_2.Add(PlayerPrefs.GetInt("value_r16:" + 0 + "," + i));

                        }
                    }
                    else
                    {
                        if(i % 2 == 0)
                        {
                            listR8_1.Add(PlayerPrefs.GetInt("value_r16:" + 1 + "," + i));

                        }
                        else
                        {
                            listR8_2.Add(PlayerPrefs.GetInt("value_r16:" + 1 + "," + i));

                        }
                    }

                }

                for(int i = 0; i < listR8_1.Count; i++)
                {
                    PlayerPrefs.SetInt("value_r8:" + 0 + "," + i, listR8_1[i]);
                    PlayerPrefs.SetInt("value_r8:" + 1 + "," + i, listR8_2[i]);
                }
                for (int i = 0; i < listR8_1.Count; i++)
                {
                    if (listR8_1[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listR8_2[i]);
                        listR8_1.RemoveAt(i);
                        listR8_2.RemoveAt(i);
                    }
                    else if (listR8_2[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listR8_1[i]);
                        listR8_1.RemoveAt(i);
                        listR8_2.RemoveAt(i);
                    }
                }

                break;

            case 6:
            case 7:
                for (int i = 0; i < 4;i++)
                {
                    if(PlayerPrefs.GetInt("Score_R8_1" + PlayerPrefs.GetInt("value_r8:" + 0 + "," + i))
                        + PlayerPrefs.GetInt("Score_R8_2" + PlayerPrefs.GetInt("value_r8:" + 0 + "," + i))
                        >= PlayerPrefs.GetInt("Score_R8_1" + PlayerPrefs.GetInt("value_r8:" + 1 + "," + i))
                        + PlayerPrefs.GetInt("Score_R8_2" + PlayerPrefs.GetInt("value_r8:" + 1 + "," + i)))
                    {
                        if( i % 2 == 0)
                        {
                            listR4_1.Add(PlayerPrefs.GetInt("value_r8:" + 0 + "," + i));
                        }
                        else
                        {
                            listR4_2.Add(PlayerPrefs.GetInt("value_r8:" + 0 + "," + i));
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            listR4_1.Add(PlayerPrefs.GetInt("value_r8:" + 1 + "," + i));
                        }
                        else
                        {
                            listR4_2.Add(PlayerPrefs.GetInt("value_r8:" + 1 + "," + i));
                        }
                    }
                }
                for(int i = 0; i < listR4_1.Count; i++)
                {
                    PlayerPrefs.SetInt("value_r4:" + 0 + "," + i, listR4_1[i]);
                    PlayerPrefs.SetInt("value_r4:" + 1 + "," + i, listR4_2[i]);
                }
                for (int i = 0; i < listR4_1.Count; i++)
                {
                    if (listR4_1[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listR4_2[i]);
                        listR4_1.RemoveAt(i);
                        listR4_2.RemoveAt(i);
                    }
                    else if (listR4_2[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        PlayerPrefs.SetInt("valueAI", listR4_1[i]);
                        listR4_1.RemoveAt(i);
                        listR4_2.RemoveAt(i);
                    }
                }
                break;

            case 8:
                listR2.Clear();
                for(int i = 0; i < 2; i++)
                {
                    if (PlayerPrefs.GetInt("Score_R4_1" + PlayerPrefs.GetInt("value_r4:" + 0 + "," + i))
                        + PlayerPrefs.GetInt("Score_R4_2" + PlayerPrefs.GetInt("value_r4:" + 0 + "," + i))
                        >=
                        PlayerPrefs.GetInt("Score_R4_1" + PlayerPrefs.GetInt("value_r4:" + 1 + "," + i))
                        + PlayerPrefs.GetInt("Score_R4_2" + PlayerPrefs.GetInt("value_r4:" + 1 + "," + i)))
                    {
                        listR2.Add(PlayerPrefs.GetInt("value_r4:" + 0 + "," + i));

                    }
                    else
                    {
                        listR2.Add(PlayerPrefs.GetInt("value_r4:" + 1 + "," + i));
                        
                    }
                }
                for (int i = 0; i < listR2.Count; i++)
                {
                    PlayerPrefs.SetInt("value_r2:" + i, listR2[i]);
                }
       
                for (int i = 0; i < listR2.Count; i++)
                {
                    if (listR2[i] == PlayerPrefs.GetInt("valuePlayer", 1))
                    {
                        listR2.RemoveAt(i);
                        PlayerPrefs.SetInt("valueAI", listR2[0]);
                    }
                }
                break;
            case 9:
                for (int i = 0; i < 2; i++)
                {

                }
                break;
                
            
        }
    }
}
