using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyBoyController : MonoBehaviour
{
    [SerializeField] GameObject ParrySpark;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Parry"))
        {
            ParrySpark.SetActive(true);
        }
    }
}
