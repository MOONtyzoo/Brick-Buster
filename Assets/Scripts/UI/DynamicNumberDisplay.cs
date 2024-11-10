using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DynamicNumberDisplay : MonoBehaviour
{
    private float currentFontScale = 1.0f;
    [SerializeField] private float maxFontScale = 1.2f;
    [SerializeField] private float fontScalePerValueUpdate = 0.03f;
    [SerializeField] private float fontScaleDecayRate = 0.05f; 

    private TextMeshProUGUI textMesh;
    private float originalFontSize;

    void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
        originalFontSize = textMesh.fontSize;
    }

    void Update() {
        DecayTextScale();
    }

    public void Reset(int newValue) {
        textMesh.text = newValue.ToString();
        SetFontScale(1.0f);
    }

    public void SetValue(int newValue) {
        textMesh.text = newValue.ToString();
        AddFontScale();
    }

    private void AddFontScale() {
        SetFontScale(currentFontScale + fontScalePerValueUpdate);
    }

    private void DecayTextScale()
    {
        if (currentFontScale != 1.0f) {
            SetFontScale(currentFontScale - fontScaleDecayRate*Time.deltaTime);
        }
    }

    private void SetFontScale(float newFontScale) {
        currentFontScale = Mathf.Clamp(newFontScale, 1.0f, maxFontScale);
        textMesh.fontSize = originalFontSize*currentFontScale;
    }
}
