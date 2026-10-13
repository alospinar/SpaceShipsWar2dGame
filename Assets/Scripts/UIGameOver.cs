using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }
    void Start()
    {
        scoreText.text = "final score: \n " + scoreKeeper.GetScore().ToString("000000000");
    }
}
