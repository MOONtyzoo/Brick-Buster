using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private Color particleColor;

    [SerializeField] private Brick brickToSpawnOnDeath;

    private bool isInvulnerable = true;

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
        if (isInvulnerable)
            return;
        
        SpawnHitParticles();
        ReplaceWithNextBrick();
        DestroyBrick();
    }

    private void SpawnHitParticles() {
        ParticleSystem particles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        ParticleSystem.MainModule particleSettings = particles.main;
        particleSettings.startColor = particleColor;
    }

    private void ReplaceWithNextBrick() {
        if (brickToSpawnOnDeath != null) {
            Brick nextBrick = Instantiate(brickToSpawnOnDeath, transform.position, Quaternion.identity);
            nextBrick.transform.SetParent(gameObject.transform.parent);
        }
    }

    private void DestroyBrick() {
        gameObject.SetActive(false);
        Destroy(gameObject);
        Game.Instance.OnBrickBusted(transform.position);
    }
}
