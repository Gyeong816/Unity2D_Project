using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyFlyer2Controller : MonoBehaviour
{

    [SerializeField] private float speed = 3f; // x축 이동 속도 (오른쪽 이동)
    [SerializeField] private float waveHeight = 1f; // 파동의 높이 (y축 진폭)
    [SerializeField] private float waveFrequency = 2f; // 파동의 주기 (진동 속도)

    private FishyFlyer2Spawner spawner;
    private Animator animator;
    private Vector3 startPos;
    private float spawnTimeOffset; // 개별 이동 시간 관리
    private bool isDead = false;

    void Start()
    {
        TryGetComponent(out animator);
        spawner = FindObjectOfType<FishyFlyer2Spawner>();
    }

    void Update()
    {
        if (isDead) return;

        // Time.time 대신 개별적인 이동 시간 사용
        float elapsedTime = Time.time - spawnTimeOffset;
        float x = startPos.x + speed * elapsedTime; //  왼쪽 → 오른쪽 이동
        float y = startPos.y + Mathf.Sin(elapsedTime * waveFrequency) * waveHeight;

        transform.position = new Vector3(x, y, startPos.z);
    }

    public void SetStartPosition(Vector3 position)
    {
        startPos = position; // 새로운 스폰 위치 설정
        transform.position = position; // 적의 위치 설정
        spawnTimeOffset = Time.time; //  이동 시간 초기화
        isDead = false;
    }

    private void OnEnable()
    {
        isDead = false; // 적이 다시 살아남
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
