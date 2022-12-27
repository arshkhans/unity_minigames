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

    public LayerMask whatIsGround;
    private BoxCollider2D coll;

    bool grounded;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        coll = GetComponent<BoxCollider2D>();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (Input.GetKey(jumpKey) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, whatIsGround);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
}
