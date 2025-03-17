using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryFishyFlyerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f; 
    [SerializeField] private float waveHeight = 1f; 
    [SerializeField] private float waveFrequency = 2f; 

    private ParryFishyFlyerSpawner spawner;
    public Animator animator;
    private Vector3 startPos;
    private float spawnTimeOffset;
    public bool isDead = false;

    void Start()
    {
        TryGetComponent(out animator);
        spawner = FindObjectOfType<ParryFishyFlyerSpawner>();
    }

    void Update()
    {
        if (isDead) return;

        float elapsedTime = Time.time - spawnTimeOffset;
        float x = startPos.x - speed * elapsedTime;
        float y = startPos.y + Mathf.Sin(elapsedTime * waveFrequency) * waveHeight;

        transform.position = new Vector3(x, y, startPos.z);
    }

    public void SetStartPosition(Vector3 position)
    {
        startPos = position; 
        transform.position = position; 
        spawnTimeOffset = Time.time;
        isDead = false;
    }

    private void OnEnable()
    {
        isDead = false; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !isDead)
        {
            isDead = true;
            animator.SetTrigger("Die");
        }
        if (collision.CompareTag("BulletBoundary") && !isDead)
        {
            isDead = true;
            animator.SetTrigger("Die");
        }
    }

    public void EnemyDie()
    {
        if (!isDead) return;
        spawner.Enqueue_enemy(gameObject);
    }
}
