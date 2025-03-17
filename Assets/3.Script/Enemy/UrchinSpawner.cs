using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrchinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy_Prefabs;
    [SerializeField] private float spawnTime = 10f; // 적이 다시 스폰될 시간
    private Queue<GameObject> enemy_queue;
    private Vector3 poolPosition = new Vector3(0, 40f, 0); // 대기 위치
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
        // 처음 적을 바로 스폰
        Dequeue_enemy();
    }

    public void Enqueue_enemy(GameObject ob)
    {
        ob.SetActive(false);
        enemy_queue.Enqueue(ob);
        // 적이 죽으면 일정 시간 뒤에 다시 스폰
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
