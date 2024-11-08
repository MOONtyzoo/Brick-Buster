using System;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform paddleTransform;
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private RingWavePropertySetter deathWaveEffect;
    
    public BallPhysics ballPhysics;

    private bool isInPlay;

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

    private void Update() {
        if (isOkToLaunch() && Input.GetButtonDown("Fire1")) {
            Launch();
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
        ballPhysics.Activate();
        ballPhysics.SetVelocity(ballPhysics.GetDesiredSpeed()*paddleTransform.up);
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
        ballPhysics.Deactivate();
    }

    public void Redirect(Vector2 newDirection) {
        ballPhysics.SetDirection(newDirection);
    }

    public void AddSpeed(float extraSpeed) {
        ballPhysics.SetSpeed(ballPhysics.GetSpeed() + extraSpeed);
    }

    public void OnBallLost() {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Instantiate(deathWaveEffect, transform.position, Quaternion.identity);
    }
}
