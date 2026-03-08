using System;
using ArtificeToolkit.Attributes;
using UnityEngine;

namespace AudioManager.Tweaks
{
    [Serializable]
    public class AudioTweaksPresetTweak :  IAppliable<AudioSource>
    {
        [InlineObject]
        public AudioTweaksPreset AudioTweaksPreset;

        public void Apply(AudioSource target) => AudioTweaksPreset?.Apply(target);
    }
}