using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    [SerializeField] private Vector3 MoveDirection = Vector3.zero;

    private void Update()
    {
        transform.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }

    public void Moveto(Vector3 direction)
    {
        MoveDirection = direction;

    }
}
