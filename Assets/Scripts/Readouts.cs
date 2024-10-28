using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Readouts : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI ballsRemainingText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI gameResultText;

    public void Reset(int startingBallCount) {
        ShowScore(0);
        ShowBallsRemaining(startingBallCount);
        ShowLevel(1);
        HideGameResult();
    }

    public void ShowScore(int score) {
        scoreText.text = $"Score: {score}";
    }

    public void ShowBallsRemaining(int ballsRemaining) {
        ballsRemainingText.text = $"Balls Remaining: {ballsRemaining}";
    }

    public void ShowLevel(int level) {
        levelText.text = $"Level: {level+1}";
    }

    public void ShowWinResult() {
        gameResultText.text = "WIN";
    }

    public void ShowLoseResult() {
        gameResultText.text = "LOSE";
    }

    public void HideGameResult() {
        gameResultText.text = "";
    }
}
