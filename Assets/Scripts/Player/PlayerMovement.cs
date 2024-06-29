using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce; 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private AudioClip jumpSound;
    private Rigidbody2D body;
    private Animator anim;
    private Health health;
    private float totalPoint;
    public UIManager ui;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private float jumpTimeCounter;
    private bool isJumping;


    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;



    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        health = FindObjectOfType<Health>();
       
    }

    private void Update()
    {
        if (isDashing) { return; }
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right
        if (horizontalInput > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Set animator's parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Move player
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash) 
        {
            StartCoroutine(Dash());
        }
        ui.ShowPoint(totalPoint.ToString());

    }
    private void FixedUpdate()
    {
        if (isDashing) { return; }
    }

    private void Jump()
    {
        SoundManager.instance.PlaySound(jumpSound);
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireHitbox"))
        {
            health.TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Point"))
        {
            totalPoint += 1;
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
