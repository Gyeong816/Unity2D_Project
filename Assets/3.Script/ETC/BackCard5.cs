using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCard5 : MonoBehaviour
{
    [SerializeField] private GameObject card5;
    [SerializeField] private float MoveSpeed = 10f;
    private Vector3 Upmove = new Vector3(0f, 1f, 0f);
    private RectTransform rectTransform;
    private Vector2 initialPosition;
    private bool isAgain = false;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        TryGetComponent(out rectTransform); 
        initialPosition = rectTransform.anchoredPosition; 
    }

    private void OnEnable()
    {
        if (isAgain)
        {
            rectTransform.anchoredPosition = initialPosition;
        }
    }
    private void Update()
    {
        MoveNext();
        Move();
    }
    private void MoveNext()
    {
        if (playerMovement.cardnum > 4)
        {
            isAgain = true;
            gameObject.SetActive(false);
        }
    }
    private void Move()
    {
        transform.position += Upmove * MoveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CardBoundary"))
        {
            isAgain = true;
            card5.SetActive(true);
            playerMovement.cardnum++;
            gameObject.SetActive(false);
        }
    }
}
