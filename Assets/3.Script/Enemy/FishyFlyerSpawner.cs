using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishyFlyerSpawner : MonoBehaviour
{

    [SerializeField] private GameObject Enemy_Prefabs;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform playerTransform;

    private Queue<GameObject> enemy_queue;
    private Vector3 poolPosition;
    private int Poolcount =5;
    private float X_offset = 10f;
    private float Y_offset = 1f;
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

    private void Start()
    {
        StartCoroutine(spawnEnemy_co());
    }


    public void Enqueue_enemy(GameObject ob)
    {
        //Destory가 대체되는 메소드
        ob.transform.position = poolPosition;
        if (ob.activeSelf)
        {
            ob.SetActive(false);
        }
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

        //  적의 시작 위치를 설정하는 메서드 호출
        FishyFlyerController fishyFlyer = enemy.GetComponent<FishyFlyerController>();
        if (fishyFlyer != null)
        {
            fishyFlyer.SetStartPosition(spawnPosition); // 새로운 위치에서 시작
        }

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
}
