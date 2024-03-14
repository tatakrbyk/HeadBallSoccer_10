using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    private GameObject _ball;
    private GameObject _player;

    private void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if(_player.GetComponent<Player>().canHead)
            {
                _player.GetComponent<Player>()._animatorPlayer.SetBool("Jump", true);

                _ball.GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 400));

            }

        }
    }
}
