using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyFlyer2Controller : MonoBehaviour
{

    [SerializeField] private float speed = 3f; // x�� �̵� �ӵ� (������ �̵�)
    [SerializeField] private float waveHeight = 1f; // �ĵ��� ���� (y�� ����)
    [SerializeField] private float waveFrequency = 2f; // �ĵ��� �ֱ� (���� �ӵ�)

    private FishyFlyer2Spawner spawner;
    private Animator animator;
    private Vector3 startPos;
    private float spawnTimeOffset; // ���� �̵� �ð� ����
    private bool isDead = false;

    void Start()
    {
        TryGetComponent(out animator);
        spawner = FindObjectOfType<FishyFlyer2Spawner>();
    }

    void Update()
    {
        if (isDead) return;

        // Time.time ��� �������� �̵� �ð� ���
        float elapsedTime = Time.time - spawnTimeOffset;
        float x = startPos.x + speed * elapsedTime; //  ���� �� ������ �̵�
        float y = startPos.y + Mathf.Sin(elapsedTime * waveFrequency) * waveHeight;

        transform.position = new Vector3(x, y, startPos.z);
    }

    public void SetStartPosition(Vector3 position)
    {
        startPos = position; // ���ο� ���� ��ġ ����
        transform.position = position; // ���� ��ġ ����
        spawnTimeOffset = Time.time; //  �̵� �ð� �ʱ�ȭ
        isDead = false;
    }

    private void OnEnable()
    {
        isDead = false; // ���� �ٽ� ��Ƴ�
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
