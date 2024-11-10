using System;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;

public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private RingWavePropertySetter deathWaveEffect;
    
    private BallPhysics ballPhysics;

    private void Awake() {
        ballPhysics = GetComponent<BallPhysics>();
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

    public void Disable() {
        gameObject.SetActive(false);
    }

    public void Reset() {
        gameObject.SetActive(true);
    }

    public void Redirect(Vector2 newDirection) {
        ballPhysics.SetDirection(newDirection);
    }

    public void AddSpeed(float extraSpeed) {
        ballPhysics.SetSpeed(ballPhysics.GetSpeed() + extraSpeed);
    }

    public void LaunchAtDesiredSpeedInDirection(Vector2 direction) {
        ballPhysics.SetVelocity(ballPhysics.GetDesiredSpeed()*direction);
    }

    public void ActivatePhysics() {
        ballPhysics.Activate();
    }

    public void DeactivatePhysics() {
        ballPhysics.Deactivate();
    }

    public void OnBallLost() {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Instantiate(deathWaveEffect, transform.position, Quaternion.identity);
    }
}
