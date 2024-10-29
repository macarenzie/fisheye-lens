using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable, VolumeComponentMenu("Menu")]
public class BlurSettings : VolumeComponent, IPostProcessComponent
{
    [Tooltip("Standard Deviation (spread) of the blur.")]
    public ClampedFloatParameter strength = new ClampedFloatParameter(0.0f, 0.0f, 15.0f);

    /// <summary>
    /// If the blur is not in effect (strength == 0.0f), does not register as active
    /// </summary>
    /// <returns></returns>
    public bool IsActive()
    {
        return (strength.value >= 0.0f) && active;
    }

    /// <summary>
    /// Outdated, but necessary to include
    /// </summary>
    /// <returns></returns>
    public bool IsTileCompatible()
    {
        return false;
    }
}
