using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrimpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy_Prefabs;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform playerTransform;

    private Queue<GameObject> enemy_queue;
    private Vector3 poolPosition;
    private int Poolcount = 5;
    [SerializeField] private float X_offset = 5f;
    [SerializeField] private float Y_offset = -3f;
    private bool isStart = false;

    private void Awake()
    {
        enemy_queue = new Queue<GameObject>();
        poolPosition = new Vector3(0, 40f, 0);
        for (int i = 0; i < Poolcount; i++)
        {
            GameObject enemy =
                Instantiate(Enemy_Prefabs, poolPosition, Quaternion.identity, transform);
            enemy.SetActive(false);
            enemy_queue.Enqueue(enemy);
        }
    }




    public void Enqueue_enemy(GameObject ob)
    {
        //Destory가 대체되는 메소드
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
        Vector3 spawnPosition = GetSpawnPosition(); // 새로운 스폰 위치 가져오기


        enemy.transform.position = spawnPosition; // 적의 위치 설정

        if (!enemy.activeSelf)
        {
            enemy.SetActive(true);
        }
    }

    private IEnumerator spawnEnemy_co()
    {

        WaitForSeconds wfs = new WaitForSeconds(spawnTime);

        while (true)
        {

            Vector3 position = new Vector3(playerTransform.position.x + X_offset, playerTransform.position.y + Y_offset, 0);

            Dequeue_enemy(position);

            yield return wfs;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isStart) // 중복 실행 방지
        {
            isStart = true;
            StartCoroutine(spawnEnemy_co()); 
        }
    }
}
