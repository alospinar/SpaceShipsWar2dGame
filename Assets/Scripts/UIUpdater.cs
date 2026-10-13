using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdater : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHelath;
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        healthSlider.maxValue = playerHelath.GetHealth();

    }

    void Update()
    {
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        healthSlider.value = playerHelath.GetHealth();
    }
}
