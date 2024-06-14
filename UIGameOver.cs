using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    ASM_MN ASM_MN;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        ASM_MN = FindObjectOfType<ASM_MN>();
    }

    private void Start()
    {
        // Lấy điểm số từ ScoreKeeper và hiển thị trên UI
        int score = scoreKeeper.GetScore();
        scoreText.text = "You Scored:\n" + score;

        // Gọi các phương thức của ASM_MN với dữ liệu cần thiết từ ScoreKeeper
        int id = scoreKeeper.GetID();
        string name = scoreKeeper.GetUserName();
        int regionId = scoreKeeper.GetIDregion();

        ASM_MN.Instance.YC1();
        ASM_MN.Instance.YC2();
        ASM_MN.Instance.YC3();
        ASM_MN.Instance.YC4();
        ASM_MN.Instance.YC5();
        ASM_MN.Instance.YC6();
        ASM_MN.Instance.YC7();
    }
}

