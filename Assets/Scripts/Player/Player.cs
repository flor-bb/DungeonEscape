using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{

    public int diamonds;

    private Rigidbody2D _rigid;
    private bool _resetJump = false;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce =5.0f;
    [SerializeField] private bool _grounded = false;
    [SerializeField] private LayerMask _groundLayer = 1<<8;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public int health;

    int IDamageable.health { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        health = 4;
    }

    void Update()
    {
        Movement();

        if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
        {
            _playerAnim.Attack();
        }
    }

    private void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");  //Input.GetAxisRaw("Horizontal");

        _grounded = IsGrounded();

        Flip(move);

        //Here we Jump
        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }
        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);

        _playerAnim.Move(move);

    }

    private void Flip(float move)
    {
        if (move > 0)
        {
            //flips the player
            _playerSprite.flipX = false;
            //flips the sword animation
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;

        }
        else if (move < 0)
        {
            //flips the player
            _playerSprite.flipX = true;
            //flips the sword animation
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;
            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    private bool IsGrounded()
    {
       RaycastHit2D hitInfo =  Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        if(hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {

        if (health <= 0)
        {
            return;
        }
        health--;
        UIManager.Instance.UpdateLives(health);

        if (health <= 0)
        {


            _playerAnim.Death();

            StartCoroutine(waitForMainMenu());

            

        }
       
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }


    IEnumerator waitForMainMenu()
    {

        //Wait for 4 seconds
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Main_Menu");

    }
}



