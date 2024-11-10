using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Readouts : MonoBehaviour
{
    [SerializeField] private DynamicNumberDisplay scoreDisplay;
    [SerializeField] private DynamicNumberDisplay ballsRemainingDisplay;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI gameResultText;

    public void Reset(int startingBallCount) {
        scoreDisplay.Reset(0);
        ballsRemainingDisplay.Reset(startingBallCount);
        ShowLevel(1);
        HideGameResult();
    }

    public void ShowScore(int score) {
        scoreDisplay.SetValue(score);
    }

    public void ShowBallsRemaining(int ballsRemaining) {
        ballsRemainingDisplay.SetValue(ballsRemaining);
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
