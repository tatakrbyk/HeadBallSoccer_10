using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject _player;
    private GameObject _AI;
    public GameObject _goals;
    public GameObject _goalsAI;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _AI = GameObject.FindGameObjectWithTag("AI");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _player.GetComponent<Player>().canShoot = true;
        }
        if (collision.gameObject.tag == "canShootAI")
        {
            _AI.GetComponent<AIPlayer>().canShootAI = true;
        }
        if(collision.gameObject.tag == "canHeadAI")
        {
            _AI.GetComponent<AIPlayer>().canHeadAI = true;
        }
        if( collision.gameObject.tag == "GoalsRight")
        {
            if(GameController.Instance.isScore == false && GameController.Instance.EndMatch == false)
            {
                Instantiate(_goals, new Vector3(0, -2, 0), Quaternion.identity);
                GameController.numberGoalsLeft++;
                GameController.Instance.isScore = true;
                GameController.Instance.ContinueMatch(false);
            }
        }
        if (collision.gameObject.tag == "GoalsLeft")
        {
            if (GameController.Instance.isScore == false && GameController.Instance.EndMatch == false)
            {
                Instantiate(_goalsAI, new Vector3(0, -2, 0), Quaternion.identity);

                GameController.numberGoalsRight++;
                GameController.Instance.isScore = true;
                GameController.Instance.ContinueMatch(true);
                
            }
        }
        if(collision.gameObject.tag == "BehindCol")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player.GetComponent<Player>().canShoot = false;
        }
        if (collision.gameObject.tag == "canShootAI")
        {
            _AI.GetComponent<AIPlayer>().canShootAI = false;
        }
        if (collision.gameObject.tag == "canHeadAI")
        {
            _AI.GetComponent<AIPlayer>().canHeadAI = false;
        }
    }
}
