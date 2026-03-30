using System;
using NnUnityEasings;

namespace Assets.Scripts.TimeScale
{
    [Serializable]
    public struct TimeScaleKey
    {
        public float TimeScale;
        public float Time;
        public Easing Easing;

        public TimeScaleKey(float timeScale = 1, float time = 0, Easing easing = Easing.Linear)
        {
            TimeScale = timeScale;
            Time = time;
            Easing = easing;
        }
    }
}
