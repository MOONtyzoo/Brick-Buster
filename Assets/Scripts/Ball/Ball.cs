using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform paddleTransform;
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private RingWavePropertySetter deathWaveEffect;
    
    private Rigidbody physics;

    private bool isInPlay;
    private float desiredSpeed = 5.0f;
    private float speedAdjustmentRate = 8.0f;

    private void Awake() {
        physics = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        PlayBounceAnimation();
        if (collision.gameObject.tag == "Brick") {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            if (!brick.isImmuneToBall) {
                brick.Bust();
            }
        }
    }

    private void PlayBounceAnimation() {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.2f*Vector3.one, 0.05f).SetEase(Ease.OutSine));
        sequence.Append(transform.DOScale(0.9f*Vector3.one, 0.1f).SetEase(Ease.InOutBounce));
        sequence.Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InBounce));
    }

    private void Update() {
        if (isOkToLaunch() && Input.GetButtonDown("Fire1")) {
            Launch();
        }

        if (isInPlay) {
            AdjustToDesiredVelocity();
        }
    }

    private bool isOkToLaunch() {
        if (isInPlay == false)
            return true;
        return false;
    }

    private void Launch() {
        isInPlay = true;
        transform.parent = null;
        physics.isKinematic = false;
        physics.velocity = desiredSpeed*paddleTransform.up;
    }

    public void Disable() {
        gameObject.SetActive(false);
    }

    public void Reset() {
        gameObject.SetActive(true);
        LoadBallOntoPaddle();
    }

    private void LoadBallOntoPaddle() {
        isInPlay = false;
        transform.parent = paddleTransform;
        transform.localPosition = new Vector3(0f, 0.5f, 0f);
        physics.isKinematic = true;
    }

    public void Redirect(Vector2 newDirection) {
        SetBallDirection(newDirection);
    }

    public void AddSpeed(float extraSpeed) {
        SetBallSpeed(GetBallSpeed() + extraSpeed);
    }

    public void AdjustToDesiredVelocity() {
        float speed = GetBallSpeed();
        float speedDifference = Math.Abs(speed - desiredSpeed);
        
        float adjustmentDirection = (speed < desiredSpeed) ? 1f : -1f;
        float speedAdjustment = speedAdjustmentRate*adjustmentDirection*Time.deltaTime;

        if (Mathf.Abs(speedAdjustment) > speedDifference) {
            SetBallSpeed(desiredSpeed);
        } else {
            SetBallSpeed(GetBallSpeed() + speedAdjustment);
        }

        print(GetBallSpeed());
    }

    public float GetBallSpeed() {
        return physics.velocity.magnitude;
    }

    public void SetBallSpeed(float newSpeed) {
        Vector2 velocityDirection = physics.velocity.normalized;

        physics.velocity = newSpeed*velocityDirection;
    }

    public Vector2 GetBallDirection() {
        return physics.velocity.normalized;
    }

    public void SetBallDirection(Vector2 newDirection) {
        float speed = physics.velocity.magnitude;

        physics.velocity = speed*newDirection;
    }

    public void OnBallLost() {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Instantiate(deathWaveEffect, transform.position, Quaternion.identity);
    }
}
