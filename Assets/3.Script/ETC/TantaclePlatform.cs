using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantaclePlatform : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    private Vector3 moveDirection = new Vector3(0f, -1f, 0f);
    private bool isMove = false;

    private void Update()
    {
        if (isMove)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMove = true;
        }
    }

    private void Move()
    {
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }

}
