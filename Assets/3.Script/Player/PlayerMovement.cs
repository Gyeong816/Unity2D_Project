using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float dashTime = 0.4f;
    [SerializeField] private float dashcoolTime = 1f;
    [SerializeField] GameObject ParrySpark;
    [SerializeField] GameObject invincibility;
    [SerializeField] private GameObject[] Cards;
    [SerializeField] private GameObject backCard1;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public bool isMove = false;

    private bool isCollected = false;
    private bool isParry = false;
    private bool isParryJump = false;
    private bool isGrounded = false;
    private bool isDashing = false;
    private bool isdashcool = false;
    public bool isDuck = false;
    public bool isAim = false;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;
    private Vector2 aimDirection = Vector2.right;
    private Vector2 shootDirection = Vector2.right;
    private PlayerHp hp;
    public int cardnum;
    private int Parrynum;
    private int Coinnum;

    private void Start()
    {
        TryGetComponent(out hp);
        TryGetComponent(out rb);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out animator);
        TryGetComponent(out capsuleCollider);
        TryGetComponent(out boxCollider);
        capsuleCollider.enabled = true;
        boxCollider.enabled = false;
    }

    private void Update()
    {
        if (hp.isDie)
        {
            return;
        }

        Move();
        HandleJump();
        HandleDash();
        HandleAim();
        HandleUpAim();
        HandleDuck();
        HandleInvincibility();
        ScoreManager.Instance.SetPlayTime(Time.timeSinceLevelLoad);
    }


    private void HandleInvincibility()
    {
        if (Input.GetKeyDown(KeyCode.V)&&cardnum>=5)
        {
            Invincibility();
        }
    }

    private void Invincibility()
    {
        invincibility.SetActive(true);
        hp.invincibility = true;
        cardnum = 0;
        for (int i = 0; i < 5; i++)
        {
            Cards[i].SetActive(false);
        }
        StartCoroutine(InvincibilityBlink_co());
        backCard1.SetActive(true);
    }

    private IEnumerator InvincibilityBlink_co()
    {
        StartCoroutine(InvincibilityTime_co());
        Color originalColor = spriteRenderer.color;
        Color BlinkColor = new Color(1f, 1f, 0.5f, 0.6f);

        spriteRenderer.color = BlinkColor;
        yield return new WaitForSeconds(7f);

        spriteRenderer.color = originalColor;
        hp.invincibility = false;
    }
    private IEnumerator InvincibilityTime_co()
    {

        yield return new WaitForSeconds(7f);
        hp.invincibility = false;
    }

    private void HandleDuck()
    {
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            isDuck = true;
            Duck();
        }
        if (Input.GetAxisRaw("Vertical") != -1)
        {
            isDuck = false;
            StandUp();
        }
    }
    private void HandleUpAim()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            animator.SetBool("UpAim", true);
        }
        if (Input.GetAxisRaw("Vertical") != 1)
        {
            animator.SetBool("UpAim", false);
            animator.SetBool("UpAimShoot", false);
        }
    }


    private void HandleAim()
    {
        if (Input.GetKey(KeyCode.C))
        {
            RockAim();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.SetBool("Aim", false);
            animator.SetBool("AimShoot", false);
            isAim = false;
        }
    }
    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isGrounded) Dash();
            else if (!isGrounded) AirDash();
        }
    }
    private void Duck()
    {
        if (isAim)
        {
            return;
        }

        animator.SetBool("Duck", true);
        capsuleCollider.enabled = false; 
        boxCollider.enabled = true;
        float moveX = Input.GetAxisRaw("Horizontal");
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("RunShoot", false);
            animator.SetBool("DuckShoot", true);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            animator.SetBool("DuckShoot", false);
        }

    }
    private void StandUp()
    {
        animator.SetBool("DuckShoot", false);
        animator.SetBool("Duck", false);
        capsuleCollider.enabled = true;  
        boxCollider.enabled = false;
    }


    private void Move()
    {
        if (isDuck || isAim || isDashing)
        {
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        if (moveX != 0)  // 오른쪽
        {
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
            isMove = true;
            animator.SetBool("Run", true);
            if (Input.GetKey(KeyCode.X))
            {
                animator.SetBool("RunShoot", true);
            }
            if (Input.GetAxisRaw("Vertical")==1)
            {
                animator.SetBool("RunDiShoot", true);
            }
            if (moveX < 0)
            {
                spriteRenderer.flipX = true;
            }
            if (moveX > 0)
            {
                spriteRenderer.flipX = false;
            }

            if (Input.GetKeyUp(KeyCode.X))
            {
                animator.SetBool("RunShoot", false);
            }
            if (Input.GetAxisRaw("Vertical") != 1)
            {
                animator.SetBool("RunDiShoot", false);
            }
        }
        else if (moveX == 0)
        {
            isMove = false;
            animator.SetBool("Run", false);
            animator.SetBool("RunShoot", false);
            animator.SetBool("RunDiShoot", false);
        }
    }

    private void RockAim()
    {
        isAim = true;
        animator.SetBool("UpAimShoot", false);
        animator.SetBool("Shoot", false);
        animator.SetBool("RunShoot", false);
        animator.SetBool("RunDiShoot", false);
        animator.SetBool("Run", false);
        animator.SetBool("Aim", true);

        float aimX = Input.GetAxisRaw("Horizontal");
        float aimY = Input.GetAxisRaw("Vertical");

        if (aimX > 0)
        {
            spriteRenderer.flipX = false;
        }
        if (aimX < 0)
        {
            spriteRenderer.flipX = true;
        }

        aimDirection = new Vector2(aimX, aimY);
        
        
        // 8방향 조준 값을 Animator로 전달 (Blend Tree 적용)
        animator.SetFloat("HorizontalAim", aimDirection.x);
        animator.SetFloat("VerticalAim", aimDirection.y);

        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("AimShoot", true);

            float shootX = Input.GetAxisRaw("Horizontal");
            float shootY = Input.GetAxisRaw("Vertical");

            if (shootX > 0)
            {
                spriteRenderer.flipX = false;
            }
            if (shootX < 0)
            {
                spriteRenderer.flipX = true;
            }

            shootDirection = new Vector2(shootX, shootY);

            animator.SetFloat("HorizontalShoot", shootDirection.x);
            animator.SetFloat("VerticalShoot", shootDirection.y);

        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isGrounded) 
            {
                Parry();
            }
            if(isGrounded)
            {
                Jump();
            }
        }
    }


    private void Jump()
    {
        if (isDuck || isAim || isDashing)
        {
            return;
        }
        isGrounded = false;

        animator.SetBool("Jump", true);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce));
    }

    private void Parry()
    {
        isParry = true;
        animator.SetBool("Parry", true);
    }
    private void ParryEnd()
    {
        isParry = false;
        animator.SetBool("Parry", false);
    }
    private void ParryJump()
    {
        isParryJump = true;
        Parrynum++;
        ScoreManager.Instance.SetParryCount(Parrynum);
        ParrySpark.SetActive(true);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce));
        cardnum++;
        UpdateCard();
    }

    private void UpdateCard()
    {
        if (cardnum <= Cards.Length)
        {
            Cards[cardnum - 1].SetActive(true);
        }  
    }
    private void Dash()
    {
        if (isdashcool||isAim||!isMove)
        {
            return;
        }
        StartDash("GroundDash");
    }

    private void AirDash()
    {
        if (isdashcool || isAim)
        {
            return;
        }

        StartDash("AirDash");
    }

    private void StartDash(string animationState)
    {
        isDashing = true;


        animator.SetBool(animationState, true);

        rb.gravityScale = 0f;
        rb.drag = 0f;
        float dashDirection = spriteRenderer.flipX ? -1 : 1;
        rb.velocity = new Vector2(dashSpeed * dashDirection, 0);

        StartCoroutine(EndDash_co());
        StartCoroutine(DashCooltime_co());

    }


    private IEnumerator EndDash_co()
    {
        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        rb.gravityScale = 3f;
        rb.drag = 3f;
        rb.velocity = new Vector2(0, 0);

        animator.SetBool("GroundDash", false);
        animator.SetBool("AirDash", false);

    }
    private IEnumerator DashCooltime_co()
    {
        isdashcool = true;
        yield return new WaitForSeconds(dashcoolTime);
        isdashcool = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Parry") && isParry && !isParryJump)
        {
            ParryJump();
        }
        if (collision.CompareTag("Coin")&& !isCollected)
        {
            isCollected = true;
            collision.gameObject.SetActive(false);
            Coinnum++;
            ScoreManager.Instance.SetCoinCount(Coinnum);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isCollected =false;
            isGrounded = true;
            isParryJump = false;
            ParrySpark.SetActive(false);
            animator.SetBool("Jump", false);
        }



        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
            transform.parent = collision.transform; 
        }


    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
            transform.parent = null; 
        }
    }
}
