using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    [SerializeField] private Text blinkingText; 
    [SerializeField] private float blinkInterval = 0.5f; 

    private void Start()
    {
        StartCoroutine(BlinkCoroutine());
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            blinkingText.enabled = !blinkingText.enabled; 
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
