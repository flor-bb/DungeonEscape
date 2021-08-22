using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rigid;
    private bool _resetJump = false;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpForce =5.0f;
    [SerializeField] private bool _grounded = false;
    [SerializeField] private LayerMask _groundLayer = 1<<8;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;


    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        _grounded = IsGrounded();

        Flip(move);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
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
            _playerSprite.flipX = false;
        }
        else if (move < 0)
        {
            _playerSprite.flipX = true;
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


}



