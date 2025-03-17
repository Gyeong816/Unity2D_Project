using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    private Vector3 upmove = new Vector3(0f, 1f, 0f);
    private Vector3 downmove = new Vector3(0f, -1f, 0f);
    private bool isMovingRight = true;
    private Animator animator;

    
    private void Start()
    {
        TryGetComponent(out animator);
    }

    private void Update()
    {
      Move();
    }

   

    private void Move()
    {
        Vector3 moveDirection;
        if (isMovingRight)
        {
            moveDirection = upmove;
        }
        else
        {
            moveDirection = downmove;
        }
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletBoundary"))
        {
            isMovingRight = !isMovingRight;
        }
    }

}
