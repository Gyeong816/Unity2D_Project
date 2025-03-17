using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public float playTime { get; private set; } // 총 플레이 시간
    public int remainingHP { get; private set; } // 남은 HP
    public int parryCount { get; private set; } // 패링 횟수
    public int coinCount { get; private set; } // 동전 획득 수

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject); // 기존 인스턴스가 있으면 제거
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

    // 플레이 시간을 00:00 (분:초) 형식으로 변환
    public string GetFormattedPlayTime()
    {
        int minutes = Mathf.FloorToInt(playTime / 60);
        int seconds = Mathf.FloorToInt(playTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
