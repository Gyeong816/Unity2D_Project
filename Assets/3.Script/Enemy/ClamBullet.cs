using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamBullet : MonoBehaviour
{
    private ClamShootController clam;
    [SerializeField] private float MoveSpeed = 10f;

    private void Start()
    {
        clam = FindAnyObjectByType<ClamShootController>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(1f, 0f, 0f); ;
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletBoundary"))
        {
            clam.Enqueue_bullet(gameObject);
        }

    }
}
