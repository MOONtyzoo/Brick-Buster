using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bumper : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteEffectsPropertySetter spriteEffectsPropertySetter;

    void Awake() {
        sprite = GetComponentInChildren<SpriteRenderer>();
        spriteEffectsPropertySetter = sprite.GetComponent<SpriteEffectsPropertySetter>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ball") {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            PlayBounceAnimation();
            ball.Redirect(ball.transform.position - transform.position);
            ball.AddSpeed(4.0f);
        }
    }

    private void PlayBounceAnimation() {
        spriteEffectsPropertySetter.SetTintAmount(1.0f);

        Tween bounceTween = sprite.transform.DOPunchScale(0.4f*Vector3.one, 0.45f, 12, 1.0f);
        Tween hitFlashTween = DOTween.To((x) => {spriteEffectsPropertySetter.SetTintAmount(x);}, 1.0f, 0.0f, 0.225f);

         Sequence animationSequence = DOTween.Sequence();
        animationSequence.Append(bounceTween);
        animationSequence.Insert(0.075f, hitFlashTween);
    }
}
