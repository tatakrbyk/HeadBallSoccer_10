using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float horizontialAxis;
    public float speed;

    private Rigidbody2D _rbPlayer;

    public bool canShoot;
    public bool grounded;
    public bool canHead;
    
    private GameObject _ball;
    public Transform checkGround;

    public LayerMask groundLayer;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _ball = GameObject.FindGameObjectWithTag("Ball");
    }

    private void Update()
    {
        horizontialAxis = Input.GetAxis("Horizontal");
        if(grounded)
        {
            canHead = false;
        }
        else
        {
            canHead = true;
        }
    }
    private void FixedUpdate()
    {
        _rbPlayer.velocity = new Vector2(Time.deltaTime * speed * horizontialAxis, _rbPlayer.velocity.y);
        grounded = Physics2D.OverlapCircle(checkGround.position, .2f, groundLayer);
    }

    public void Move(int value)
    {
        if (GameController.Instance.isScore == false && GameController.Instance.EndMatch == false)
        {
            horizontialAxis = value;
        }
    }
    
    public void StopMove()
    {
        horizontialAxis = 0;
    }

    public void Shoot()
    {
        if(canShoot)
        {
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(320, 400));
        }
    }

    public void Jump()
    {
        if(grounded && GameController.Instance.isScore == false && GameController.Instance.EndMatch == false)
        {
            canHead = true;
            _rbPlayer.velocity = new Vector2(_rbPlayer.velocity.x, 15);
        }
    }
}
