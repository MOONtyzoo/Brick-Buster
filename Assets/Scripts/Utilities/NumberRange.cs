using UnityEngine;

public class NumberRange
{
    public float minValue;
    public float maxValue;

    public NumberRange(float minValue, float maxValue) {
        this.minValue = minValue;
        this.maxValue = maxValue;
    }

    public static NumberRange Combine(NumberRange a, NumberRange b) {
        float minValue = Mathf.Min(a.minValue, b.minValue);
        float maxValue = Mathf.Max(a.maxValue, b.maxValue);
        return new NumberRange(minValue, maxValue);
    }

    public float ClampToRange(float value) {
        return Mathf.Clamp(value, minValue, maxValue);
    }

    public bool IsValueInsideRange(float value) {
        return value >= minValue && value <= maxValue;
    }
    
    public bool IsValueOutsideRange(float value) {
        return value < minValue || value > maxValue;
    }
    
    public float GetRandomNumber() {
        return Random.Range(minValue, maxValue);
    }
}
