using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject FishyFlyer1_Spawner;
    [SerializeField] GameObject FishyFlyer2_Spawner;
    [SerializeField] GameObject FishyFlyer3_Spawner;
    [SerializeField] GameObject ShrimpSpawner1;
    [SerializeField] GameObject ShrimpSpawner2;
    [SerializeField] GameObject Clam;



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("StartFishy"))
        {
            FishyFlyer1_Spawner.SetActive(true);
            FishyFlyer2_Spawner.SetActive(true);
            FishyFlyer3_Spawner.SetActive(true);
        }
        if (collision.CompareTag("EndFishy"))
        {
            FishyFlyer1_Spawner.SetActive(false);
            FishyFlyer2_Spawner.SetActive(false);
            FishyFlyer3_Spawner.SetActive(false);
        }
        if (collision.CompareTag("EndUI"))
        {
            ShrimpSpawner1.SetActive(false);
            ShrimpSpawner2.SetActive(false);
            Clam.SetActive(false);
        }
    }
}
