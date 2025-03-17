using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrchinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy_Prefabs;
    [SerializeField] private float spawnTime = 10f; // ���� �ٽ� ������ �ð�
    private Queue<GameObject> enemy_queue;
    private Vector3 poolPosition = new Vector3(0, 40f, 0); // ��� ��ġ
    private Vector3 spawnPosition;
    private int Poolcount = 5;

    private void Awake()
    {
        spawnPosition = transform.position;
        enemy_queue = new Queue<GameObject>();

        for (int i = 0; i < Poolcount; i++)
        {
            GameObject enemy = Instantiate(Enemy_Prefabs, poolPosition, Quaternion.identity, transform);
            enemy.SetActive(false);
            enemy_queue.Enqueue(enemy);
        }
    }

    private void Start()
    {
        // ó�� ���� �ٷ� ����
        Dequeue_enemy();
    }

    public void Enqueue_enemy(GameObject ob)
    {
        ob.SetActive(false);
        enemy_queue.Enqueue(ob);
        // ���� ������ ���� �ð� �ڿ� �ٽ� ����
        Invoke(nameof(Dequeue_enemy), spawnTime);
    }

    public void Dequeue_enemy()
    {
        if (enemy_queue.Count == 0) return;

        GameObject enemy = enemy_queue.Dequeue();
        enemy.transform.position = spawnPosition;
        enemy.SetActive(true);
    }
}
