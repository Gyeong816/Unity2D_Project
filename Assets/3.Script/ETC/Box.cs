using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    [SerializeField] private float MoveTime = 1f;
    private Vector3 moveDirection = new Vector3(0f, -1f, 0f);
    private Vector3 moveupDirection = new Vector3(0f, 1f, 0f);
    private bool isMove = false;
    private bool isMoveUp = false;

    private void Update()
    {
        if (isMove)
        {
            Move();
        }
        if (isMoveUp)
        {
            MoveUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMove = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isMoveUp = true;
        }
    }
    private void Move()
    {
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
        StartCoroutine(MoveTime_co());
    }
    private void MoveUp()
    {
        transform.position += moveupDirection * MoveSpeed * Time.deltaTime;
        StartCoroutine(MoveupTime_co());
    }

    private IEnumerator MoveTime_co()
    {
        yield return new WaitForSeconds(MoveTime);
        isMove = false;
    }
    private IEnumerator MoveupTime_co()
    {
        yield return new WaitForSeconds(MoveTime);
        isMoveUp = false;
    }
}
