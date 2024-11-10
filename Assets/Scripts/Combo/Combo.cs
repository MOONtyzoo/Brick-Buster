using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Combo : MonoBehaviour
{
    private static ComboState currentComboState;
    private static Dictionary<ComboStateType, ComboState> comboStates = new Dictionary<ComboStateType, ComboState>() {
        {ComboStateType.red,
        new ComboState(ComboStateType.red,
            pointMultiplier:1.0f,
            ballSpeed:5.0f,
            comboDecayRate: 0.5f
        )},

        {ComboStateType.blue,
        new ComboState(ComboStateType.blue,
            pointMultiplier:1.5f,
            ballSpeed:6.0f,
            comboDecayRate: 1.0f
        )},

        {ComboStateType.green,
        new ComboState(ComboStateType.green,
            pointMultiplier:2.0f,
            ballSpeed:6.5f,
            comboDecayRate: 1.2f
        )},

        {ComboStateType.yellow,
        new ComboState(ComboStateType.yellow,
            pointMultiplier:2.5f,
            ballSpeed:7.0f,
            comboDecayRate: 1.5f
        )},

        {ComboStateType.rainbow,
        new ComboState(ComboStateType.rainbow,
            pointMultiplier:4.0f,
            ballSpeed:7.5f,
            comboDecayRate: 2.0f
        )},
    };
    
    private float comboPoints = 0;
    private static Dictionary<ComboStateType, NumberRange> comboStatePointRanges = new Dictionary<ComboStateType, NumberRange> {
        {ComboStateType.red, new NumberRange(0, 5)},
        {ComboStateType.blue, new NumberRange(5, 10)},
        {ComboStateType.green, new NumberRange(10, 15)},
        {ComboStateType.yellow, new NumberRange(15, 20)},
        {ComboStateType.rainbow, new NumberRange(20, 25)},
    };
    private static NumberRange comboPointRange = NumberRange.Combine(comboStatePointRanges[ComboStateType.red], comboStatePointRanges[ComboStateType.rainbow]);


    void Awake() {
        Initialize();
    }

    public static void Initialize()
    {
        currentComboState = comboStates[ComboStateType.red];
    }

    void Update() {
        DecayComboPoints();
        if (Input.GetKeyDown(KeyCode.E)) {
            AddComboPoints(1f);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            AddComboPoints(-1f);
        }
    }

    private void DecayComboPoints() {
        
    }

    public void AddComboPoints(float value) {
        SetComboPoints(comboPoints + value);
    }

    public void SetComboPoints(float newComboPoints) {
        comboPoints = comboPointRange.ClampToRange(newComboPoints);
        print(comboPoints);
        CheckIfShouldUpdateComboState();
    }

    private void CheckIfShouldUpdateComboState()
    {
        NumberRange comboStateRange = comboStatePointRanges[currentComboState.type];
        if (comboStateRange.IsValueOutsideRange(comboPoints)) {
            UpdateComboState();
        }
    }

    private void UpdateComboState() {
        foreach (ComboState comboStateRange in comboStates.Values) {
            NumberRange comboRange = comboStatePointRanges[comboStateRange.type];
            if (comboRange.IsValueInsideRange(comboPoints)) {
                TransitionToComboState(comboStateRange.type);
            }
        }
    }

    private void TransitionToComboState(ComboStateType newComboStateType)
    {
        if (comboStates.TryGetValue(newComboStateType, out ComboState newComboState))
        {
            string currentComboStateName = currentComboState.type.ToString();
            string newComboStateName = newComboStateType.ToString();
            print("Transitioning from " + currentComboStateName + " to " + newComboStateName);
            currentComboState.Deactivate();
            newComboState.Activate();
            currentComboState = newComboState;
        }
    }
}
