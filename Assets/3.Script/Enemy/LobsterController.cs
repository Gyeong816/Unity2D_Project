using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    private Vector3 rightmove = new Vector3(1f, 0f, 0f);
    private Vector3 leftmove = new Vector3(-1f, 0f, 0f);
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isMovingRight = true;
    [SerializeField] private bool isStart = false;

    private void Start()
    {

        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
    }

    private void Update()
    {
        if (isStart)
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
    }

    private void FlipDirection()
    {
        isMovingRight = !isMovingRight;

        if (isMovingRight)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

    }
}
