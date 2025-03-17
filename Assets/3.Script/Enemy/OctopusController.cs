using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusController : MonoBehaviour
{
    [SerializeField] GameObject Clam;
    [SerializeField] GameObject ClamShoot1;
    [SerializeField] GameObject ClamShoot2;
    [SerializeField] private float MoveSpeed = 1f;
    private Vector3 rightmove = new Vector3(1f, 0f, 0f);
    private Vector3 downmove = new Vector3(0f, -1f, 0f);
    private bool isMove = false;
    private bool isdownMove = false;

    private void Update()
    {
        if (isMove&&!isdownMove)
        {
            Move();
        }
        if (isdownMove)
        {
            MoveDown();
        }
    }

    private void Move()
    {
        transform.position += rightmove * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Clam.SetActive(true);
            ClamShoot1.SetActive(true);
            ClamShoot2.SetActive(true);
            isMove = true;
        }
        if (collision.CompareTag("Boundary"))
        {
            isMove = false;
            isdownMove = true;
        }
    }

    private void MoveDown()
    {
        transform.position += downmove * MoveSpeed * Time.deltaTime;
    }

}
