using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private PlayerShooting playerShooting;

    public void SetPlayerShooting(PlayerShooting shooting)
    {
        playerShooting = shooting;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            ReturnToPool();
        }
        if (collision.CompareTag("BulletBoundary"))
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);

        if (playerShooting != null) 
        {
            playerShooting.ReturnBulletToPool(gameObject); 
        }
    }
}
