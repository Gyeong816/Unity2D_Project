using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnacleBullet2 : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private float angle = 1;
    private BarnacleController barnacle;
    private bool isShoot = false;
    private void Start()
    {
        barnacle = FindAnyObjectByType<BarnacleController>();
        TryGetComponent(out rb);
        Bulletshoot();
    }

    private void Update()
    {
        if (!isShoot)
        {
            Bulletshoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BoundaryGround"))
        {
            barnacle.Enqueue_bullet2(gameObject);
            isShoot = false;
        }
    }

    private void Bulletshoot()
    {
        isShoot = true;
        rb.AddForce(new Vector2(angle, jumpForce));
    }
}
