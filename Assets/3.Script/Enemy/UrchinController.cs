using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrchinController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    private Vector3 rightmove = new Vector3(1f, 0f, 0f);
    private Vector3 leftmove = new Vector3(-1f, 0f, 0f);
    private SpriteRenderer spriteRenderer;
    private bool isMovingRight = true;
    private bool isDead = false;
    private Animator animator;
    private UrchinSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<UrchinSpawner>();


        TryGetComponent(out animator);
 

        TryGetComponent(out spriteRenderer);

    }

    private void OnEnable()
    {
        isDead = false;
        if (animator != null)
        {
            animator.ResetTrigger("Die");
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 moveDirection;
        if (isMovingRight)
        {
            moveDirection = rightmove;
        }
        else
        {
            moveDirection = leftmove;
        }
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            FlipDirection();
        }
        if (collision.CompareTag("Bullet") && !isDead)
        {
            isDead = true;

            animator.SetTrigger("Die");
            
            Invoke(nameof(EnemyDie), 0.5f);
        }
    }

    private void FlipDirection()
    {
        isMovingRight = !isMovingRight;

        if (isMovingRight)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

    }

    public void EnemyDie()
    {
     
        spawner.Enqueue_enemy(gameObject);
 
    }
}
