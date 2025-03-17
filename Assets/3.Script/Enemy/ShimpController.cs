using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShimpController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float angle = 1;
    private Rigidbody2D rb;
    private ShrimpSpawner spawner;
    private Animator animator;
    private bool isDead = false;
    private bool isJump = false;


    void Start()
    {
        TryGetComponent(out rb);
        TryGetComponent(out animator);
        spawner = FindObjectOfType<ShrimpSpawner>();
    }

    void Update()
    {
        if (!isJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        isJump = true;
        rb.AddForce(new Vector2(angle, jumpForce));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;
        if (collision.CompareTag("Bullet") && !isDead)
        {
            isDead = true;
            animator.SetTrigger("Die");
        }
        if (collision.CompareTag("Boundary"))
        {
            animator.SetTrigger("Die");
        }
    }

    public void EnemyDie()
    {
        spawner.Enqueue_enemy(gameObject);
        isJump = false;
        isDead = false;
    }
}
