using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHead : MonoBehaviour
{
    private GameObject _ball;

    private void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            //_ball.GetComponent<Rigidbody>().velocity = new Vector2(0,0); 
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-300, 400));

        }
    }
}
