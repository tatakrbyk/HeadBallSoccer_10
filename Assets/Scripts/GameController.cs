using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameController : MonoBehaviour
{
    private static GameController _instance; public static GameController Instance { get { return _instance; } }


    public TextMeshProUGUI textRightScore;
    public TextMeshProUGUI textLeftScore;
    public TextMeshProUGUI textTimeMatch;

    public float timeMatch;

    public int numberGoalsRight;
    public int numberGoalsLeft;

    public bool isScore;
    public bool EndMatch;

    private GameObject _ball;
    private GameObject _player;
    private GameObject _AI;
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

        _ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectWithTag("Player");
        _AI = GameObject.FindGameObjectWithTag("AI");

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
}
