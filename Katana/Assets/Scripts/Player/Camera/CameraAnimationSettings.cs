using System;
using NnUnityEasings;
using UnityEngine;

[Serializable]
public class CameraAnimationSettings
{
    public float Duration;
    public Easing Easing;
    public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1, 1);

    public CameraAnimationSettings(float duration = 0, Easing easing = Easing.Linear)
    {
        Duration = duration;
        Easing = easing;
        Curve = AnimationCurve.Linear(0, 0, 1, 1);
    }

    public CameraAnimationSettings(float duration, Easing easing, AnimationCurve curve)
    {
        Duration = duration;
        Easing = easing;
        Curve = curve;
    }
}
