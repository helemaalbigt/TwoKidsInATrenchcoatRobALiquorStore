using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseAlphaGruop : MonoBehaviour {
    public CanvasGroup group;
    public float factor = 5f;
    public float minAlpha = 0.5f;
    void Update() {
        var f = (Mathf.Sin(Time.unscaledTime * factor) + 1f) / 2f;
        group.alpha = Mathf.Lerp(minAlpha, 1f, f);
    }
}
