using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClamShootController : MonoBehaviour
{
    [SerializeField] private GameObject Bullet_Prefabs;
    private Vector3 poolPosition = new Vector3(0, 40f, 0);
    private Queue<GameObject> bullet_queue;
    private int Poolcount = 4;


    private void Awake()
    {
        bullet_queue = new Queue<GameObject>();
        for (int i = 0; i < Poolcount; i++)
        {
            GameObject bullet = Instantiate(Bullet_Prefabs, poolPosition, Quaternion.identity, transform);
            bullet.SetActive(false);
            bullet_queue.Enqueue(bullet);
        }
    }

    public void Enqueue_bullet(GameObject ob)
    {
        ob.SetActive(false);
        bullet_queue.Enqueue(ob);
    }
    public void Dequeue_bullet()
    {
        GameObject bullet = bullet_queue.Dequeue();
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Clam"))
        {
            Dequeue_bullet();
        }
    }
}
