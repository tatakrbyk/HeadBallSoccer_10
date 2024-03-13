using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class AIPlayer : MonoBehaviour
{
    public float rangeOfDefence;
    public float speed;

    public Transform defencePos;
    public Transform checkGround;

    private GameObject _ball;
    private GameObject _player;

    private Rigidbody2D _rbAI;

    public bool canShootAI;
    public bool canHeadAI;
    public bool grounded;

    public LayerMask groundLayer;


    private void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectWithTag("Player");
        _rbAI = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameController.Instance.isScore == false && GameController.Instance.EndMatch == false)
        {
            Move();
            if (canShootAI)
            {
                Shoot();
            }
            if(canHeadAI && grounded)
            {
                Jump();
            }

        }
    }
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(checkGround.position, 0.2f, groundLayer);
    }

    public void Move()
    {
        if(Mathf.Abs(_ball.transform.position.x - transform.position.x) < rangeOfDefence)
        {
            if (Mathf.Abs(_ball.transform.position.x - transform.position.x) <= Mathf.Abs(_ball.transform.position.x - _player.transform.position.x) 
                && _ball.transform.position.y < -1f && _ball.transform.position.x < transform.position.x)
            {
                _rbAI.velocity = new Vector2(Time.deltaTime * speed, _rbAI.velocity.y);
            }
            else
            {
                if (_ball.transform.position.x < transform.position.x && transform.position.y < -1f) // Check >
                {
                    _rbAI.velocity = new Vector2(Time.deltaTime * speed, _rbAI.velocity.y);
                }
                else if (_ball.transform.position.y >= 1f && transform.position.x >= defencePos.position.x)
                {
                    _rbAI.velocity = new Vector2(0, _rbAI.velocity.y);
                }
                else
                {
                    _rbAI.velocity = new Vector2(-Time.deltaTime * speed, _rbAI.velocity.y);
                }
            }
        }
        else
        {
            if(transform.position.x > defencePos.position.x)
            {
                _rbAI.velocity = new Vector2(-Time.deltaTime * speed, _rbAI.velocity.y);
            }
            else
            {
                _rbAI.velocity = new Vector2(0, _rbAI.velocity.y) ;
            }
        }
    }

    public void Shoot()
    {
        //_ball.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
        _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50, 00));

    }

    public void Jump()
    {
        _rbAI.velocity = new Vector2(_rbAI.velocity.x, 15);
    }
}
