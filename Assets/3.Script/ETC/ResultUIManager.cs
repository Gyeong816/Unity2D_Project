using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private Text playTimeText;
    [SerializeField] private Text remainingHPText;
    [SerializeField] private Text parryCountText;
    [SerializeField] private Text coinCountText;
    [SerializeField] private Text GradeAlphabet;

    [SerializeField] private Image Circle;

    private float NextTime=1f;

    private void Start()
    {
        StartCoroutine(DisplayResults());
    }

    IEnumerator DisplayResults()
    {
        yield return new WaitForSeconds(NextTime);
        // 1. TIME 표시
        playTimeText.text = "TIME.................." + ScoreManager.Instance.GetFormattedPlayTime();
        playTimeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 2. HP 표시
        remainingHPText.text = "HP.........................." + ScoreManager.Instance.remainingHP + "/8";
        remainingHPText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 3. PARRY 표시
        parryCountText.text = "PARRY...................." + ScoreManager.Instance.parryCount;
        parryCountText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 4. GOLD COINS 표시
        coinCountText.text = "GOLD COINS........" + ScoreManager.Instance.coinCount + "/6";
        coinCountText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 5. Grade 계산 및 표시
        string grade = CalculateGrade(); // 등급 계산
        GradeAlphabet.text = " " + grade;
        GradeAlphabet.gameObject.SetActive(true);
        Circle.gameObject.SetActive(true);
    }

    //  점수를 계산하여 A~F 등급을 매기는 함수
    private string CalculateGrade()
    {
        int hp = ScoreManager.Instance.remainingHP;  // 남은 HP
        int parry = ScoreManager.Instance.parryCount; // 패링 횟수
        int coins = ScoreManager.Instance.coinCount; // 코인 개수

        //  점수 계산 (기준 설정)
        int totalScore = (hp * 10) + (parry * 5) + (coins * 20);

        // 등급 매기기 (점수에 따라 A~F 결정)
        if (totalScore >= 150) return "A";
        else if (totalScore >= 120) return "B";
        else if (totalScore >= 90) return "C";
        else if (totalScore >= 60) return "D";
        else if (totalScore >= 30) return "E";
        else return "F";
    }
}
