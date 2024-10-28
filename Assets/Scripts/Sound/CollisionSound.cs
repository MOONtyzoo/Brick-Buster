using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    [SerializeField] private AudioClip soundEffect;

    private void OnCollisionEnter(Collision collision) {
        Sounds.Instance.PlaySoundEffect(soundEffect, transform);
    }
}
