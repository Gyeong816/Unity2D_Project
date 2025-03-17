using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public float playTime { get; private set; } // �� �÷��� �ð�
    public int remainingHP { get; private set; } // ���� HP
    public int parryCount { get; private set; } // �и� Ƚ��
    public int coinCount { get; private set; } // ���� ȹ�� ��

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject); // ���� �ν��Ͻ��� ������ ����
        }
    }
    public void SetPlayTime(float playTime)
    {
        this.playTime = playTime;
    }

    public void SetRemainingHP(int remainingHP)
    {
        this.remainingHP = remainingHP;
    }

    public void SetParryCount(int parryCount)
    {
        this.parryCount = parryCount;
    }

    public void SetCoinCount(int coinCount)
    {
        this.coinCount = coinCount;
    }

    // �÷��� �ð��� 00:00 (��:��) �������� ��ȯ
    public string GetFormattedPlayTime()
    {
        int minutes = Mathf.FloorToInt(playTime / 60);
        int seconds = Mathf.FloorToInt(playTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
