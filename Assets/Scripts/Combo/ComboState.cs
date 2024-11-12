using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ComboState
{
    public ComboStateType type { get; private set; }
    public float pointMultiplier { get; private set; }
    public float ballSpeed { get; private set; }
    public float comboDecayRate { get; private set; }
    public float comboDecayCooldown { get; private set; }
    public Color32 comboColor { get; private set; }
    public float ballHueShift { get; private set; }

    public bool isActive { get; private set; }

    private List<Action> activationCallList = new List<Action>();
    private List<Action> deactivationCallList = new List<Action>();

    public ComboState(ComboStateType type, float pointMultiplier, float ballSpeed, float comboDecayRate, float comboDecayCooldown,
    Color32 comboColor, float ballHueShift)
    {
        isActive = false;
        this.type = type;
        this.comboColor = comboColor;
        this.pointMultiplier = pointMultiplier;
        this.ballSpeed = ballSpeed;
        this.ballHueShift = ballHueShift;
        this.comboDecayRate = comboDecayRate;
        this.comboDecayCooldown = comboDecayCooldown;
    }

    public void Activate()
    {
        if (isActive) return;
        
        isActive = true;
        foreach (Action action in activationCallList)
        {
            action.Invoke();
        }
    }

    public void Deactivate()
    {
        if (!isActive) return;
        
        isActive = false;
        foreach (Action action in deactivationCallList)
        {
            action.Invoke();
        }
    }

    public void AddToActivationCallList(Action action)
    {
        activationCallList.Add(action);
    }

    public void AddToDeactivationCallList(Action action)
    {
        deactivationCallList.Add(action);
    }
}
