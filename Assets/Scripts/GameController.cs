using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
public class GameController : MonoBehaviour
{
    private static GameController _instance; public static GameController Instance { get { return _instance; } }


    public TextMeshProUGUI textRightScore;
    public TextMeshProUGUI textLeftScore;
    public TextMeshProUGUI textTimeMatch;

    public float timeMatch;

    public static int numberGoalsRight;
    public static int numberGoalsLeft;

    public bool isScore;
    public bool EndMatch;

    private GameObject _ball;
    private GameObject _player;
    private GameObject _AI;

    public Image flagLeft;
    public Image flagRight;

    public TextMeshProUGUI nameLeft;
    public TextMeshProUGUI nameRight;

    // Initialization element player and UI

    public SpriteRenderer headPlayer;
    public SpriteRenderer shoePlayer;
    public SpriteRenderer bodyPlayer;

    public SpriteRenderer headAI;
    public SpriteRenderer shoeAI;
    public SpriteRenderer bodyAI;

    public GameObject pausePanel;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        
    }
    private void Start()
    {
        timeMatch = 90;
        numberGoalsLeft = 0;
        numberGoalsRight = 0;

        _ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectWithTag("Player");
        _AI = GameObject.FindGameObjectWithTag("AI");

        flagLeft.sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        nameLeft.text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];

        flagRight.sprite = UITeam.Instance.flagTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
        nameRight.text = UITeam.Instance.nameTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];

        headPlayer.sprite = UITeam.Instance.head[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        bodyPlayer.sprite = UITeam.Instance.body[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        shoePlayer.sprite = UITeam.Instance.shoe[Random.Range(0,4)];

        headAI.sprite = UITeam.Instance.head[PlayerPrefs.GetInt("valueAI", 1) - 1];
        bodyAI.sprite = UITeam.Instance.body[PlayerPrefs.GetInt("valueAI", 1) - 1];
        shoeAI.sprite = UITeam.Instance.shoe[Random.Range(0, 4)];

        StartCoroutine(BeginMatch());
    }

    private void Update()
    {
        textLeftScore.text = numberGoalsLeft.ToString();
        textRightScore.text = numberGoalsRight.ToString();
        textTimeMatch.text = timeMatch.ToString();
    }

    private IEnumerator BeginMatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(timeMatch > 0)
            {
                timeMatch--;
                
            }
            else
            {
                StartCoroutine(WaitEndGame());

                EndMatch = true;
                break;
            }
        }
    }

    public void ContinueMatch(bool winPlayer)
    {
        StartCoroutine(WaitContinueMatch(winPlayer));    
    }

    private IEnumerator WaitContinueMatch(bool winPlayer)
    {
        yield return new WaitForSeconds(2f);
        isScore = false;
        if(EndMatch == false)
        {
            _ball.transform.position = new Vector3(0,0,0);
            _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

            _player.transform.position = new Vector3(-5,0,0);
            _AI.transform.position = new Vector3(5,0,0);

            if(winPlayer) // give ball of goal
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 200));
            }
            else
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));
            }
        }
    }

    public void ButtonPause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ButtonResume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ButtonLose()
    {
        numberGoalsRight = 3;
        numberGoalsLeft = 0;
        timeMatch = 0;
        pausePanel.SetActive(false);
        Time.timeScale =  1f;
        StartCoroutine(WaitEndGame());
    }

    private IEnumerator WaitEndGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("EndGama");
    }
}
