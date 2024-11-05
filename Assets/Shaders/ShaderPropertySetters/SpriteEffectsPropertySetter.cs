using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    See https://www.ronja-tutorials.com/post/048-material-property-blocks/

    The script is set up this way to allow altering the material properties
    on a per instance basis instead of it applying to everything using the material.
*/

public class SpriteEffectsPropertySetter : MonoBehaviour
{
    [SerializeField] private Color TintColor = Color.white;

    [Range(0f, 1f)]
    [SerializeField] private float TintAmount;

    [Range(0f, 6.28318531f)]
    [SerializeField] private float HueShift;

    [Range(0f, 5f)]
    [SerializeField] private float Saturation = 1;

    [Range(-1f, 1f)]
    [SerializeField] private float Brightness;

    new private Renderer renderer;
    //The material property block we pass to the GPU
    private MaterialPropertyBlock propertyBlock;

    // OnValidate is called in the editor after the component is edited
    void OnValidate()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (propertyBlock == null) {
            propertyBlock = new MaterialPropertyBlock();
        }
        if (renderer.HasPropertyBlock()) {
            renderer.GetPropertyBlock(propertyBlock);
        }

        propertyBlock.SetColor("_TintColor", TintColor);
        propertyBlock.SetFloat("_TintAmount", TintAmount);
        propertyBlock.SetFloat("_HueShift", HueShift);
        propertyBlock.SetFloat("_Saturation", Saturation);
        propertyBlock.SetFloat("_Brightness", Brightness);

        renderer.SetPropertyBlock(propertyBlock);
    }

    void Awake() {
        renderer = GetComponent<Renderer>();
        if (propertyBlock == null) {
            propertyBlock = new MaterialPropertyBlock();
        }
        if (renderer.HasPropertyBlock()) {
            renderer.GetPropertyBlock(propertyBlock);
        }
    }

    public void SetTintColor(Color newTintColor) {
        TintColor = newTintColor;
        if (propertyBlock != null) {
            propertyBlock.SetColor("_TintColor", TintColor);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }

    public void SetTintAmount(float newTintAmount) {
        TintAmount = newTintAmount;
        if (propertyBlock != null) {
            propertyBlock.SetFloat("_TintAmount", TintAmount);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }

    public void SetHueShift(float newHueShift) {
        HueShift = newHueShift;
        if (propertyBlock != null) {
            propertyBlock.SetFloat("_HueShift", HueShift);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }

    public void SetSaturation(float newSaturation) {
        Saturation = newSaturation;
        if (propertyBlock != null) {
            propertyBlock.SetFloat("_Saturation", Saturation);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }

    public void SetBrightness(float newBrightness) {
        Brightness = newBrightness;
        if (propertyBlock != null) {
            propertyBlock.SetFloat("_Brightness", Brightness);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}
