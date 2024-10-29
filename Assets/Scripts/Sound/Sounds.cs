using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using UnityEngine;

/*
    https://www.youtube.com/watch?v=DU7cgVsU2rM

    Followed this tutorial to make a simple sound manager to handle AudioSource instances.
    The main reason is that the sounds of brick collisions need to exist after the brick is destroyed.
    This one also lets you play multiple sounds simultaneously.
*/

public class Sounds : MonoBehaviour
{
    public static Sounds Instance {get; private set;}

    [SerializeField] private AudioSource soundEffectPrefab;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffect(AudioClip audioClip, Transform spawnTransform, float volume=1f) {
        AudioSource soundEffect = Instantiate(soundEffectPrefab, spawnTransform.position, Quaternion.identity);
        soundEffect.transform.SetParent(gameObject.transform);

        soundEffect.clip = audioClip;
        soundEffect.volume = volume;

        soundEffect.Play();

        float audioDuration = audioClip.length;
        Destroy(soundEffect.gameObject, audioDuration);
    }
}
