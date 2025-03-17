using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject StartUI;
    [SerializeField] private GameObject EndUI;
    private PlayerHp HP;

    private void Start()
    {
        StartCoroutine(StartUI_co());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndUI"))
        {
            StartCoroutine(EndUI_co());
        }
    }


    private IEnumerator StartUI_co()
    {
        StartUI.SetActive(true); 
        yield return new WaitForSeconds(1f); 

        StartUI.SetActive(false); 
    }

    private IEnumerator EndUI_co()
    {
        EndUI.SetActive(true);
        yield return new WaitForSeconds(2f); 

        EndUI.SetActive(false);
        SceneManager.LoadScene("Results");
    }
}
