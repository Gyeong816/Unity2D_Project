using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    [SerializeField] GameObject BlackScreen;

    private void End_Invincibility()
    {
        gameObject.SetActive(false);
    }
    private void startScreen()
    {
        BlackScreen.SetActive(true);
    }
    private void endScreen()
    {
        BlackScreen.SetActive(false);
    }
}
