using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Score scoreScript;
    [SerializeField] private TMP_Text scoreText;

    private const string SCORE_TEXT_PREFIX = "Score: ";
    private void Update()
    {
        scoreText.text = SCORE_TEXT_PREFIX + scoreScript.GetScore().ToString();
    }
}
