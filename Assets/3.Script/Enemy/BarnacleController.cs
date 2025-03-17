using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnacleController : MonoBehaviour
{
    [SerializeField] private GameObject Bullet1_Prefabs;
    [SerializeField] private GameObject Bullet2_Prefabs;
    [SerializeField] private GameObject Bullet3_Prefabs;
    private Collider2D enemyCollider;
    [SerializeField] private float Barnacle_HP = 10f;
    private Queue<GameObject> bullet1_queue;
    private Queue<GameObject> bullet2_queue;
    private Queue<GameObject> bullet3_queue;
    private Vector3 poolPosition = new Vector3(0, 40f, 0); 
    private Vector3 spawnPosition;

    private Vector3[] spawnPositions = new Vector3[]
       {
           new Vector3(0, 0, 0),
        new Vector3(34.73f, -0.71f, 0),
        new Vector3(55.17f, -10.31f, 0),
        new Vector3(139.5f, -14.55f, 0),
        new Vector3(0f, -60f, 0)// 추가적인 리스폰 위치
       };
    private int currentSpawnIndex = 0; 

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private bool isHit = false;
    private int Poolcount = 1;
    private void Awake()
    {
        spawnPosition = transform.position;
        bullet1_queue = new Queue<GameObject>();
        bullet2_queue = new Queue<GameObject>();
        bullet3_queue = new Queue<GameObject>();

        for (int i = 0; i < Poolcount; i++)
        {
            GameObject bullet1 = Instantiate(Bullet1_Prefabs, poolPosition, Quaternion.identity, transform);
            bullet1.SetActive(false);
            bullet1_queue.Enqueue(bullet1);
        }
        for (int i = 0; i < Poolcount; i++)
        {
            GameObject bullet2 = Instantiate(Bullet2_Prefabs, poolPosition, Quaternion.identity, transform);
            bullet2.SetActive(false);
            bullet2_queue.Enqueue(bullet2);
        }
        for (int i = 0; i < Poolcount; i++)
        {
            GameObject bullet3 = Instantiate(Bullet3_Prefabs, poolPosition, Quaternion.identity, transform);
            bullet3.SetActive(false);
            bullet3_queue.Enqueue(bullet3);
        }
    }
    private void Start()
    {
        TryGetComponent(out enemyCollider);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !isDead && !isHit)
        {
            Take_Damage();
        }
    }

    private void Take_Damage()
    {
        Barnacle_HP -= 1;

        if (Barnacle_HP <= 0)
        {
            isDead = true;
            enemyCollider.enabled = false;
            animator.SetBool("Die", true);

            if (spriteRenderer != null)
                spriteRenderer.color = Color.white;
        }
        else
        {
            if (!isHit) 
            {
                isHit = true;
                StartCoroutine(TakeDamage_co());
            }
        }
    }

    private IEnumerator TakeDamage_co()
    {
        Color originalColor = spriteRenderer.color; 
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.6f); 

        yield return new WaitForSeconds(0.1f);

        if (!isDead) 
        {
            spriteRenderer.color = originalColor;
        }

        isHit = false; 
    }

    public void shoot()
    {
        Dequeue_bullet1();
        Dequeue_bullet2();
        Dequeue_bullet3();
    }

    public void Enqueue_bullet1(GameObject ob_1)
    {
        ob_1.SetActive(false);
        bullet1_queue.Enqueue(ob_1);
    }
    public void Enqueue_bullet2(GameObject ob_2)
    {
        ob_2.SetActive(false);
        bullet2_queue.Enqueue(ob_2);
    }
    public void Enqueue_bullet3(GameObject ob_3)
    {
        ob_3.SetActive(false);
        bullet3_queue.Enqueue(ob_3);
    }

    public void Dequeue_bullet1()
    {
        if (bullet1_queue.Count == 0) return;

        GameObject bullet1 = bullet1_queue.Dequeue();
        bullet1.transform.position = spawnPosition;
        bullet1.SetActive(true);
    }
    public void Dequeue_bullet2()
    {
        if (bullet2_queue.Count == 0) return;

        GameObject bullet2 = bullet2_queue.Dequeue();
        bullet2.transform.position = spawnPosition;
        bullet2.SetActive(true);
    }
    public void Dequeue_bullet3()
    {
        if (bullet3_queue.Count == 0) return;

        GameObject bullet3 = bullet3_queue.Dequeue();
        bullet3.transform.position = spawnPosition;
        bullet3.SetActive(true);
    }
    public void RespawnEnemy()
    {
        // 다음 리스폰 위치 선택 (순차적으로 반복)
        currentSpawnIndex = (currentSpawnIndex + 1) % spawnPositions.Length;

        transform.position = spawnPositions[currentSpawnIndex];
        spawnPosition = transform.position;  //  총알 발사 위치도 변경
        Barnacle_HP = 10f; //  체력 회복
        enemyCollider.enabled = true;
        animator.SetBool("Die", false);
        isDead = false;

    }
}
