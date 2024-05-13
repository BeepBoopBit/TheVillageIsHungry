using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private  TMP_Text _text;

    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _boxCollider;
    private float _wallJumpCooldown;
    private float _horizontalInput;

    private float startingX;
    private float startingY;

    public Image _heart01;
    public Image _heart02;
    public Image _heart03;
    

    private int _heartNum = 0;
    private int _score = 0;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startingX = this.transform.position.x;
        startingY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        _horizontalInput = Input.GetAxis("Horizontal");

        // Flip the Player in realtes to his movement
        if(_horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(_horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1,1,1);
        }

        // set animation
        _anim.SetBool("isRunning", _horizontalInput != 0);
        _anim.SetBool("isGrounded", isGrounded());

        // Wall Jumping
        if(_wallJumpCooldown > 0.2f)
        {
            _body.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed, _body.velocity.y);
            if(onWall() && !isGrounded())
            {
                _body.gravityScale = 1f;
                _body.velocity = Vector2.zero;
            }
            else
            {
                _body.gravityScale = 2f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x, _jumpPower);
            _anim.SetTrigger("jump");
        }
        else if (onWall() && !isGrounded())
        {
            if(_horizontalInput == 0)
            {
                _body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 1.2f, 0);
                transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
            }
            else
            {
                _body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 2f, 3f);
            }
            _wallJumpCooldown = 0;
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 0.07f, _groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.07f, _wallLayer);
        return raycastHit.collider != null;
    }

    private void returnToStart()
    {
        this.transform.position = new Vector2(startingX, startingY);
    }

    public void lossHeart(string tagValue)
    {
        if (tagValue == "Enemy")
        {
            if (_heartNum == 0)
            {
                _heart01.GetComponent<Animator>().SetBool("isLossHeart", true);
                _heartNum++;
            }
            else if (_heartNum == 1)
            {
                _heart02.GetComponent<Animator>().SetBool("isLossHeart", true);
                _heartNum++;
            }
            else if (_heartNum == 2)
            {
                _heart03.GetComponent<Animator>().SetBool("isLossHeart", true);
                _heartNum++;
            }
            else
            {
                SceneManager.LoadScene(0);
            }
            returnToStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Fruit")
        {
            Destroy(collision.gameObject);
            _score += 1;
            _text.text = "x " + _score.ToString();
        }
        if(collision.tag == "Trophy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        lossHeart(collision.tag);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        lossHeart(collision.gameObject.tag);
    }
}
