using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cinemachine;
using UnityEngine;

/*
    https://www.youtube.com/watch?v=ACf1I27I6Tk&t=337s

    Created following this tutorial for camera shake
*/

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance {get; private set;}

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTimer;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        } else {
            Destroy(gameObject);
        }
    }

    public void ShakeCamera(float intensity, float time) {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = 
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void Update() {
        if (shakeTimer > 0) {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0) {
                ShakeCamera(0, 0);
            }
        }
    }
}
