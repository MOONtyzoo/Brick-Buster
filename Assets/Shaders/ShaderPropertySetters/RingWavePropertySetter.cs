using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    See https://www.ronja-tutorials.com/post/048-material-property-blocks/

    The script is set up this way to allow altering the material properties
    on a per instance basis instead of it applying to everything using the material.
*/

public class RingWavePropertySetter : MonoBehaviour
{
    [SerializeField] float uvScale = 1.0f;

    [Header("Time Settings")][Space]
    [SerializeField] float timeScale = 1.0f;
    [SerializeField] float lifetime = 1.0f;
    private float startTime = 0.0f;

    [Header("Ring Settings")][Space]
    [SerializeField] int ringNumber = 1;
    [SerializeField] float ringSpawnDelay = 0.5f;

    [Header("Speed Settings")][Space]
    [SerializeField] float ringStartSpeed = 1.0f;
    [SerializeField] float ringDeceleration = 0.0f;

    [Header("Fade Settings")][Space]
    [SerializeField] Color ringColor = Color.white;
    [SerializeField] float fadeStartTime = 99999.0f;
    [SerializeField] float fadeDuration = 1.0f;

    [Header("Width Settings")][Space]
    [SerializeField] float ringStartWidth = 0.1f;
    [SerializeField] float ringThinningStartTime = 99999.0f;
    [SerializeField] float ringThinningDuration = 1.0f;

    [Header("Angle Settings")][Space]
    [Range(0.0f, 360.0f)]
    [SerializeField] float ringAngleSpread = 360.0f;

    new private Renderer renderer;
    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    void OnValidate() {
        UpdateShaderProperties();
    }

    void UpdateShaderProperties() {
        Renderer renderer = GetComponent<Renderer>();

        if (propertyBlock == null) {
            propertyBlock = new MaterialPropertyBlock();
        }
        if (renderer.HasPropertyBlock()) {
            renderer.GetPropertyBlock(propertyBlock);
        }

        propertyBlock.SetFloat("_uvScale", uvScale);

        propertyBlock.SetFloat("_TimeScale", timeScale);
        propertyBlock.SetFloat("_Lifetime", lifetime);
        propertyBlock.SetFloat("_StartTime", startTime);

        propertyBlock.SetInteger("_RingNumber", ringNumber);
        propertyBlock.SetFloat("_RingSpawnDelay", ringSpawnDelay);

        propertyBlock.SetFloat("_RingStartSpeed", ringStartSpeed);
        propertyBlock.SetFloat("_RingDeceleration", ringDeceleration);

        propertyBlock.SetColor("_RingColor", ringColor);
        propertyBlock.SetFloat("_FadeStartTime", fadeStartTime);
        propertyBlock.SetFloat("_FadeDuration", fadeDuration);

        propertyBlock.SetFloat("_RingStartWidth", ringStartWidth);
        propertyBlock.SetFloat("_RingThinningStartTime", ringThinningStartTime);
        propertyBlock.SetFloat("_RingThinningDuration", ringThinningDuration);

        propertyBlock.SetFloat("_RingAngleSpread", ringAngleSpread);

        renderer.SetPropertyBlock(propertyBlock);
    }

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        
        startTime = Time.time;
        UpdateShaderProperties();
        Destroy(gameObject, lifetime);
    }

    public void SetRingColor(Color newRingColor) {
        ringColor = newRingColor;
        if (propertyBlock != null) {
            propertyBlock.SetColor("_RingColor", ringColor);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
