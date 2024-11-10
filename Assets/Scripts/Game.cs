using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance {get; private set;}

    [SerializeField] private Readouts readouts;
    [SerializeField] private ScoreFloater scoreFloaterPrefab;
    [SerializeField] private Paddle paddle;
    [SerializeField] private Ball ball;
    [SerializeField] private BallLauncher ballLauncher;

    [Header("Sounds")]
    [SerializeField] private AudioClip gameStartSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip ballLostSound;


    private Levels levels;

    private int score;
    private int ballsRemaining;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            levels = GetComponent<Levels>();
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Reset();
        levels.LoadStartLevel();
        Sounds.Instance.PlaySoundEffect(gameStartSound, transform);
    }

    private void Reset() {
        score = 0;
        ballsRemaining = 3;
        readouts.Reset(ballsRemaining);
        ResetAfterBallLoss();
    }

    public void OnBrickBusted() {
        if (levels.IsLevelCompleted())
            OnLevelCompleted();
    }

    public void LoseBall() {
        UpdateBallsRemaining(ballsRemaining-1);
        ball.OnBallLost();
        DisableGameplay();
        CheckForGameOver();
        Sounds.Instance.PlaySoundEffect(ballLostSound, transform);
    }

    public void DisableGameplay() {
        ball.Disable();
        paddle.Disable();
    }

    private void OnLevelCompleted() {
        if (!levels.IsLastLevel()) {
            levels.GoToNextLevel();
            ResetAfterBallLoss();
        } else {
            WinGame();
        }
    }

    private void CheckForGameOver() {
        if (ballsRemaining != 0) {
            ResetAfterBallLoss();
        } else {
            LoseGame();
        }
    }

    private void ResetAfterBallLoss() {
        paddle.Reset();
        ball.Reset();
        ballLauncher.LoadBall();
    }

    private void WinGame() {
        readouts.ShowWinResult();
        DisableGameplay();
    }

    private void LoseGame() {
        readouts.ShowLoseResult();
        DisableGameplay();
        Sounds.Instance.PlaySoundEffect(gameOverSound, transform);
    }

    private void UpdateBallsRemaining(int val) {
        ballsRemaining = val;
        readouts.ShowBallsRemaining(ballsRemaining);
    }

    public void AddScoreWithFloater(int val, Vector3 floaterPos) {
        UpdateScore(score + val);
        Instantiate(scoreFloaterPrefab, floaterPos, Quaternion.identity);
    }

    private void UpdateScore(int val) {
        score = val;
        readouts.ShowScore(score);
    }
}
