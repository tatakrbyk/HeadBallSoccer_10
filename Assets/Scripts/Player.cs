using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float horizontialAxis;
    public float speed;

    private Rigidbody2D rigidbodyPlayer;

    public bool canShoot;
    public bool grounded;
    
    private GameObject _ball;
    public Transform checkGround;

    public LayerMask groundLayer;
    private void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        _ball = GameObject.FindGameObjectWithTag("Ball");
    }

    private void Update()
    {
        horizontialAxis = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        rigidbodyPlayer.velocity = new Vector2(Time.deltaTime * speed * horizontialAxis, rigidbodyPlayer.velocity.y);
        grounded = Physics2D.OverlapCircle(checkGround.position, .2f, groundLayer);
    }

    public void Move(int value)
    {
        horizontialAxis = value;
    }
    
    public void StopMove()
    {
        horizontialAxis = 0;
    }

    public void Shoot()
    {
        if(canShoot)
        {
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(400, 500));
        }
    }

    public void Jump()
    {
        if(grounded)
        {
            rigidbodyPlayer.velocity = new Vector2(rigidbodyPlayer.velocity.x, 15);
        }
    }
}
