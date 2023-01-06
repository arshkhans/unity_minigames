using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDPlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Audio")]
    [SerializeField] private AudioSource jumpSoundEffect;

    public LayerMask whatIsGround;
    private BoxCollider2D coll;

    private bool isStatic = true;

    private enum MovementState { idle, running, jumping, falling }

    private float horizontalInput;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void MovePlayer()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (Input.GetKey(jumpKey) && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

    private void UpdateAnimation()
    {
        MovementState state;

        if (horizontalInput > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        } else if (horizontalInput < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        } else 
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .01f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.01f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int) state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, whatIsGround);
    }

    private void staticPlayer()
    {
        isStatic = true;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void dynamicPlayer()
    {
        isStatic = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStatic)
            MovePlayer();
        UpdateAnimation();
    }
}
