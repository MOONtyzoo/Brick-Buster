using System;
using DG.Tweening;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer constrainingSpriteRenderer;

    [SerializeField] private GameObject paddle;

    public float moveSpeed;

    public float maximumTurnAngle = 60;
    private float currentTurnAngle = 0;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball") {
            PlayBallHitAnimation();
        }
    }

    private void PlayBallHitAnimation() {
        Transform paddleTransform = paddle.transform;

        Sequence scaleSequence = DOTween.Sequence();
        scaleSequence.Append(paddleTransform.DOScale(0.9f*Vector3.one, 0.03f).SetEase(Ease.OutSine));
        scaleSequence.Append(paddleTransform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutSine));

        // Sequence localMoveSequence = DOTween.Sequence();
        // localMoveSequence.Append(paddleTransform.DOLocalMoveY(-0.1f, 0.02f).SetEase(Ease.OutSine));
        // localMoveSequence.Append(paddleTransform.DOLocalMoveY(0.03f, 0.05f).SetEase(Ease.InOutSine));
        // localMoveSequence.Append(paddleTransform.DOLocalMoveY(0f, 0.07f).SetEase(Ease.InSine));

        Sequence animationSequence = DOTween.Sequence();
        animationSequence.Append(scaleSequence);
        animationSequence.Insert(0f, paddleTransform.DOPunchPosition(0.18f*Vector3.down, 0.12f, 10, 15));
    }

    private void Update() {
        ProcessPlayerInput();
        RotateToMouseCursor();
        ConstrainToScreen();
    }

    private void ProcessPlayerInput() {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Vector3 movementVector = moveSpeed*horizontalAxis*Vector3.right*Time.deltaTime;
        transform.position += movementVector;
    }

    private void RotateToMouseCursor() {
        Vector2 dirToMousePos = GetMousePosition() - (Vector2)transform.position;
        float targetAngle = Vector2.SignedAngle(dirToMousePos, Vector2.up);
        SetTurnAngle(targetAngle);
    }

    private Vector2 GetMousePosition() {
        Vector3 mouseScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }

    private void SetTurnAngle(float newTurnAngle) {
        currentTurnAngle = Math.Clamp(newTurnAngle, -maximumTurnAngle, maximumTurnAngle);
        Vector2 directionFromAngle = Quaternion.AngleAxis(currentTurnAngle, Vector3.back) * Vector2.up;
        transform.up = directionFromAngle;
    }

    private void ConstrainToScreen() {
        transform.position = SpriteTools.ConstrainSpriteToScreen(constrainingSpriteRenderer);
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    public void Reset() {
        gameObject.SetActive(true);
    }
}
