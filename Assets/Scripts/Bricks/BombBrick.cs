using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombBrick : Brick
{
    [SerializeField] private RingWavePropertySetter explosionRingWave;

    [Header("Explosion hitbox properties")]
    [SerializeField] private ExplosionHitbox explosionHitbox;
    [SerializeField] private float explosionRadius = 3.0f;
    [SerializeField] private float explosionSpeed = 10.0f;

    protected override void PlayDestroyEffects() {
        SpawnExplosion();
        SpawnHitParticles();
        SpawnRingWave();
        CinemachineShake.Instance.ShakeCamera(1.8f, 0.35f);
        PlayDestroyAnimation();
    }

    private void SpawnExplosion() {
        ExplosionHitbox explosion = Instantiate(explosionHitbox, transform.position, Quaternion.identity);
        explosion.hitboxGrowthSpeed = explosionSpeed;
        explosion.hitboxEndRadius = explosionRadius;
    }

    private void SpawnRingWave() {
        Instantiate(explosionRingWave, transform.position, Quaternion.identity);
    }

    protected override void PlayDestroyAnimation()
    {
        base.PlayDestroyAnimation();
    }
}
