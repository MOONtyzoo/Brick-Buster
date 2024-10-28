using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ParticleSystem particlePrefab;

    public void Bust() {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(gameObject);
        Game.Instance.OnBrickBusted(transform.position);
    }
}
