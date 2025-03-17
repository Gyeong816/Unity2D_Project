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
        // 1. TIME ǥ��
        playTimeText.text = "TIME.................." + ScoreManager.Instance.GetFormattedPlayTime();
        playTimeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 2. HP ǥ��
        remainingHPText.text = "HP.........................." + ScoreManager.Instance.remainingHP + "/8";
        remainingHPText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 3. PARRY ǥ��
        parryCountText.text = "PARRY...................." + ScoreManager.Instance.parryCount;
        parryCountText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 4. GOLD COINS ǥ��
        coinCountText.text = "GOLD COINS........" + ScoreManager.Instance.coinCount + "/6";
        coinCountText.gameObject.SetActive(true);
        yield return new WaitForSeconds(NextTime);

        // 5. Grade ��� �� ǥ��
        string grade = CalculateGrade(); // ��� ���
        GradeAlphabet.text = " " + grade;
        GradeAlphabet.gameObject.SetActive(true);
        Circle.gameObject.SetActive(true);
    }

    //  ������ ����Ͽ� A~F ����� �ű�� �Լ�
    private string CalculateGrade()
    {
        int hp = ScoreManager.Instance.remainingHP;  // ���� HP
        int parry = ScoreManager.Instance.parryCount; // �и� Ƚ��
        int coins = ScoreManager.Instance.coinCount; // ���� ����

        //  ���� ��� (���� ����)
        int totalScore = (hp * 10) + (parry * 5) + (coins * 20);

        // ��� �ű�� (������ ���� A~F ����)
        if (totalScore >= 150) return "A";
        else if (totalScore >= 120) return "B";
        else if (totalScore >= 90) return "C";
        else if (totalScore >= 60) return "D";
        else if (totalScore >= 30) return "E";
        else return "F";
    }
}
