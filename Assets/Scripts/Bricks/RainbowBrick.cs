using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowBrick : MonoBehaviour
{
    private float hueShift = 0.0f;

    private SpriteEffectsPropertySetter spriteEffectsPropertySetter;

    void Awake() {
        spriteEffectsPropertySetter = GetComponent<SpriteEffectsPropertySetter>();
    }

    void Update()
    {
        hueShift = hueShift + 2.5f*Time.deltaTime;
        if (hueShift >= 2.0f*(float)Math.PI) {
            hueShift -= 2.0f*(float)Math.PI;
        }
        
        spriteEffectsPropertySetter.SetHueShift(hueShift);
    }
}
