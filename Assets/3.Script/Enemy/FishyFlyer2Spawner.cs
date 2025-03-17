using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyFlyer2Spawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy_Prefabs;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform playerTransform;

    private Queue<GameObject> enemy_queue;
    private Vector3 poolPosition;
    private int Poolcount = 5;
    private float X_offset = -10f; // ���ʿ��� �����ϵ��� ����
    private float Y_offset = 1f;

    private void Awake()
    {
        enemy_queue = new Queue<GameObject>();
        poolPosition = new Vector3(0, 40f, 0);
        for (int i = 0; i < Poolcount; i++)
        {
            GameObject enemy = Instantiate(Enemy_Prefabs, poolPosition, Quaternion.identity, transform);
            enemy.SetActive(false);
            enemy_queue.Enqueue(enemy);
        }
    }

    private void Start()
    {
        StartCoroutine(spawnEnemy_co());
    }

    public void Enqueue_enemy(GameObject ob)
    {
        ob.transform.position = poolPosition;
        ob.SetActive(false);
        enemy_queue.Enqueue(ob);
    }

    public Vector3 GetSpawnPosition()
    {
        return new Vector3(playerTransform.position.x + X_offset, playerTransform.position.y + Y_offset, 0);
    }

    public void Dequeue_enemy(Vector2 position)
    {
        if (enemy_queue.Count <= 0)
        {
            GameObject ob = Instantiate(Enemy_Prefabs, poolPosition, Quaternion.identity, transform);
            enemy_queue.Enqueue(ob);
        }

        GameObject enemy = enemy_queue.Dequeue();
        Vector3 spawnPosition = GetSpawnPosition(); // ���ο� ���� ��ġ ��������

        // ���� ��ġ�� ���� �����ϰ� SetStartPosition ȣ��
        enemy.transform.position = spawnPosition;

        // ���� ���� ��ġ�� �����ϴ� �޼��� ȣ��
        FishyFlyer2Controller fishyFlyer = enemy.GetComponent<FishyFlyer2Controller>();
        if (fishyFlyer != null)
        {
            fishyFlyer.SetStartPosition(spawnPosition); // ���ο� ��ġ���� ����
        }

        enemy.SetActive(true);
    }

    private IEnumerator spawnEnemy_co()
    {
        WaitForSeconds wfs = new WaitForSeconds(spawnTime);

        while (true)
        {
            Vector3 position = GetSpawnPosition();
            Dequeue_enemy(position);
            yield return wfs;
        }
    }
}
