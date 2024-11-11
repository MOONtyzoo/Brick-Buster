using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisplay : MonoBehaviour
{
    private float currentFontScale = 1.0f;
    [SerializeField] private float maxFontScale = 1.2f;
    [SerializeField] private float fontScalePerValueUpdate = 0.03f;
    [SerializeField] private float fontScaleDecayRate = 0.05f;

    private TextMeshProUGUI textMesh;
    private float originalFontSize;

    [SerializeField] private RectTransform comboPointsBar;
    [SerializeField] private float comboPointsBarHeight;

    void Awake() {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        originalFontSize = textMesh.fontSize;

        SetBarProgress(0.25f);
    }

    void Update() {
        DecayTextScale();
    }

    public void Reset(int newValue) {
        textMesh.text = newValue.ToString();
        SetFontScale(1.0f);
    }

    public void SetMultiplier(float newValue) {
        // "n1" parses float with 1 decimal place
        textMesh.text = "x" + newValue.ToString("n1");
        AddFontScale();
    }

    public void SetBarProgress(float progressPercentage) {
        progressPercentage = Mathf.Clamp(progressPercentage, 0.0f, 1.0f);

        float newOffsetMaxY = -comboPointsBarHeight * (1.0f-progressPercentage);
        comboPointsBar.offsetMax = new Vector2(comboPointsBar.offsetMax.x, newOffsetMaxY);
    }

    public void SetColor(Color32 color) {
        textMesh.color = color;
        comboPointsBar.GetComponent<Image>().color = color;
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
