using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private Color particleColor;

    [SerializeField] private Brick brickToSpawnOnDeath;
    public bool isImmuneToBall = false;
    public bool isImmuneToExplosions = false;

    private SpriteRenderer spriteRenderer;
    private BoxCollider boxCollider;
    private SpriteEffectsPropertySetter spriteEffectsPropertySetter;

    private bool isInvulnerable = true;
    private bool isDestroyed = false;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        spriteEffectsPropertySetter = GetComponent<SpriteEffectsPropertySetter>();
    }

    public void Start() {
        StartCoroutine(invulnerabilityDebounce());
    }

    /*
        When colliding at glancing angles, the ball would sometimes break multiple layers
        in a single "visible" collision. This debounce fixes that.
    */
    private IEnumerator invulnerabilityDebounce() {
        yield return new WaitForSeconds(0.1f);
        isInvulnerable = false;
    }

    public void Bust() {
        if (isInvulnerable || isDestroyed)
            return;
        
        ReplaceWithNextBrick();
        PlayDestroyEffects();
    }

    private void ReplaceWithNextBrick() {
        if (brickToSpawnOnDeath != null) {
            Brick nextBrick = Instantiate(brickToSpawnOnDeath, transform.position + 0.01f*Vector3.forward, Quaternion.identity);
            nextBrick.transform.SetParent(gameObject.transform.parent);
        }
    }

    protected virtual void PlayDestroyEffects() {
        SpawnHitParticles();
        CinemachineShake.Instance.ShakeCamera(0.5f, 0.1f);
        Game.Instance.AddScore(10, transform.position);
        PlayDestroyAnimation();
    }

    protected void SpawnHitParticles() {
        ParticleSystem particles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        ParticleSystem.MainModule particleSettings = particles.main;
        particleSettings.startColor = particleColor;
    }
    
    protected virtual void PlayDestroyAnimation() {
        isDestroyed = true;
        boxCollider.enabled = false;

        Tween shakeTween = transform.DOPunchScale(0.38f*Vector3.one, 0.3f, 50, 0.3f);
        
        spriteEffectsPropertySetter.SetTintColor(Color.white);
        spriteEffectsPropertySetter.SetTintAmount(1.0f);
        Tween hitFlashTween = DOTween.To((x) => {spriteEffectsPropertySetter.SetTintAmount(x);}, 1.0f, 0.0f, 0.225f);
        
        Sequence animationSequence = DOTween.Sequence();
        animationSequence.Append(shakeTween);
        animationSequence.Insert(0.075f, hitFlashTween);
        animationSequence.OnComplete(DestroyBrick);
    }

    private void DestroyBrick() {
        gameObject.SetActive(false);
        Destroy(gameObject);
        Game.Instance.OnBrickBusted();
    }
}
