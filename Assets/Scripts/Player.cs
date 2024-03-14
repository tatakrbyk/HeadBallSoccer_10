using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject buttonShoot;
    public GameObject buttonMoveLeft;
    public GameObject buttonMoveRight;
    public GameObject buttonJump;

    public Animator _animatorPlayer;

    public int hashShoot;
    public int hashJump;
    public int hashMoveLeft;
    public int hashMoveRight;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        _ball = GameObject.FindGameObjectWithTag("Ball");

        hashShoot = Animator.StringToHash("Shoot");
        hashJump = Animator.StringToHash("Jump");
        hashMoveLeft = Animator.StringToHash("MoveLeft");
        hashMoveRight = Animator.StringToHash("MoveRight");
    }

    private void Update()
    {
        horizontialAxis = Input.GetAxis("Horizontal");
        if(grounded)
        {
            canHead = false;
            _animatorPlayer.SetBool(hashJump, false);

        }
        else
        {
            canHead = true;
            _animatorPlayer.SetBool(hashMoveRight, false);
            _animatorPlayer.SetBool(hashMoveLeft, false);

        }
    }
    private void FixedUpdate()
    {
        _rbPlayer.velocity = new Vector2(Time.deltaTime * speed * horizontialAxis, _rbPlayer.velocity.y);
        grounded = Physics2D.OverlapCircle(checkGround.position, .2f, groundLayer);
    }

    public void Move(int value)
    {
        if(value == 1)
        {
            _animatorPlayer.SetBool(hashMoveRight, true);

            buttonMoveRight.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            buttonMoveRight.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        }
        if (value == -1)
        {
            _animatorPlayer.SetBool(hashMoveLeft, true);

            buttonMoveLeft.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            buttonMoveLeft.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        }

        if (GameController.Instance.isScore == false && GameController.Instance.EndMatch == false)
        {
            horizontialAxis = value;
        }
    }
    
    public void StopMoveLeft(int value)
    {
        if (value == 1)
        {
            _animatorPlayer.SetBool(hashMoveRight, false);

            buttonMoveRight.transform.localScale = new Vector3(1f, 1f, 1f);
            buttonMoveRight.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
        }
        if (value == -1)
        {
            _animatorPlayer.SetBool(hashMoveLeft, false);

            buttonMoveLeft.transform.localScale = new Vector3(1f, 1f, 1f);
            buttonMoveLeft.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);

        }
        horizontialAxis = 0;
    }

    public void Shoot()
    {
        _animatorPlayer.SetTrigger(hashShoot);

        buttonShoot.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        buttonShoot.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);
        if(canShoot)
        {
            _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(9, 9 * Mathf.Tan(_ball.GetComponent<Ball>().angleOrientBall * Mathf.Deg2Rad)), ForceMode2D.Impulse);
        }
    }

    public void StopShoot()
    {
        buttonShoot.transform.localScale = new Vector3(1f, 1f, 1f);
        buttonShoot.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
    }
    public void Jump()
    {
        if(grounded && GameController.Instance.isScore == false && GameController.Instance.EndMatch == false)
        {
            buttonJump.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
            buttonJump.GetComponent<Image>().CrossFadeAlpha(0.4f, 0.1f, true);

            canHead = true;
            _rbPlayer.velocity = new Vector2(_rbPlayer.velocity.x, 15);
        }
    }

    public void StopJump()
    {
        buttonJump.transform.localScale = new Vector3(1f, 1f, 1f);
        buttonJump.GetComponent<Image>().CrossFadeAlpha(1f, 0.1f, true);
    }
    private void OnDisable()
    {
        _animatorPlayer.SetBool(hashJump, false);
    }
}
