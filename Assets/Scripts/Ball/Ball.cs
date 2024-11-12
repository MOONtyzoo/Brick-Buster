using System;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;

public class Ball : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private RingWavePropertySetter deathWaveEffect;
    [SerializeField] private RingWavePropertySetter comboWaveEffect;
    
    private BallPhysics ballPhysics;
    private SpriteEffectsPropertySetter spriteEffectsPropertySetter;

    private ComboStateType comboState;

    private float animationHueShift;

    private void Awake() {
        ballPhysics = GetComponent<BallPhysics>();
        spriteEffectsPropertySetter = GetComponent<SpriteEffectsPropertySetter>();
    }

    private void Update() {
        if (comboState == ComboStateType.rainbow) {
            AnimateRainbow();
        }
    }

    private void AnimateRainbow()
    {
        animationHueShift = animationHueShift + 2.5f*Time.deltaTime;
        if (animationHueShift >= 2.0f*(float)Math.PI) {
            animationHueShift -= 2.0f*(float)Math.PI;
        }
        spriteEffectsPropertySetter.SetHueShift(animationHueShift);
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

    private void PlayComboTransitionAnimation(Color comboColor) {
        RingWavePropertySetter comboWave = Instantiate(comboWaveEffect, transform.position, Quaternion.identity);
        comboWave.SetRingColor(comboColor);

        spriteEffectsPropertySetter.SetTintAmount(1.0f);
        spriteEffectsPropertySetter.SetTintColor(comboColor);
        Tween hitFlashTween = DOTween.To((x) => {spriteEffectsPropertySetter.SetTintAmount(x);}, 1.0f, 0.0f, 0.225f);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.3f*Vector3.one, 0.08f).SetEase(Ease.OutSine));
        sequence.Append(transform.DOScale(0.85f*Vector3.one, 0.13f).SetEase(Ease.InOutBounce));
        sequence.Append(transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.InBounce));
        sequence.Insert(0.075f, hitFlashTween);
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

    public void SetComboState (ComboState comboState) {
        ComboStateType previousComboState = this.comboState;
        this.comboState = comboState.type;
        spriteEffectsPropertySetter.SetHueShift(comboState.ballHueShift);
        ballPhysics.SetDesiredSpeed(comboState.ballSpeed);
        if (comboState.type > previousComboState) {
            PlayComboTransitionAnimation(comboState.comboColor);
        }
    }

    public void OnBallLost() {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Instantiate(deathWaveEffect, transform.position, Quaternion.identity);
    }
}
