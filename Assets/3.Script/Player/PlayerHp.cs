using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] GameObject firepoint;
    [SerializeField] GameObject ghost;
    [SerializeField] public int Player_HP = 8;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public bool isDie = false;
    public bool isTakingDamage = false;
    public bool invincibility = false;
    [SerializeField] private Image healthUI; 
    [SerializeField] private Sprite[] healthSprites; 
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject YouDiedUI;



    private void Start()
    {
        UpdateHealthUI();
        ghost.SetActive(false);
        deathUI.SetActive(false);
        YouDiedUI.SetActive(false);
        TryGetComponent(out animator);
        TryGetComponent(out spriteRenderer);
        TryGetComponent(out playerMovement);
    }

    private void Take_Damage()
    {
        Player_HP -= 1;
        isTakingDamage = false;
        UpdateHealthUI();

        if (Player_HP <= 0)
        {
            Player_Die();
        }
        else
        {
            animator.Play("Player_Idle", 0, 0);
        }
    }

    private void UpdateHealthUI()
    {
        if (Player_HP >= 0)
        {
            healthUI.sprite = healthSprites[(int)Player_HP]; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie) return;

        if (collision.CompareTag("Enemy") && !invincibility)
        {
            isTakingDamage = true;
            invincibility = true;
            animator.SetTrigger("Hit");
            StartCoroutine(InvincibilityBlink_co());
        }
        if (collision.CompareTag("Water"))
        {
            Player_Die();
        }
        if (collision.CompareTag("EndUI"))
        {
            ScoreManager.Instance.SetRemainingHP(Player_HP);
        }
    }

    private IEnumerator InvincibilityBlink_co()
    {
        StartCoroutine(invincibilityTime_co());
        Color originalColor = spriteRenderer.color;
        Color BlinkColor = new Color(1f, 1f, 1f, 0.5f);
        while (invincibility)
        {
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(0.2f);

            spriteRenderer.color = BlinkColor;
            yield return new WaitForSeconds(0.2f);
        }
        spriteRenderer.color = originalColor;

    }
    private IEnumerator invincibilityTime_co()
    {
        yield return new WaitForSeconds(2f); // 1초 동안 유지
        invincibility = false; 
    }

    private void Player_Die()
    {
        isDie = true;
        firepoint.SetActive(false);
        animator.SetTrigger("Die");
        ghost.SetActive(true);

        StartCoroutine(ShowDeathUI());
    }

    private IEnumerator ShowDeathUI()
    {
        YouDiedUI.SetActive(true); //  "YOU DIED" UI 표시
        yield return new WaitForSeconds(2f); //  2초 대기

        YouDiedUI.SetActive(false); //  "YOU DIED" UI 숨기기
        deathUI.SetActive(true); //  최종 사망 UI 표시
    }
}
