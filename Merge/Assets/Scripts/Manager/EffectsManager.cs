using MergeHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void VibrationWithDelay(long milliseconds, float timer) // #param1 Duration, #param2 Delay
    {
        StartCoroutine(VibrateDelay(milliseconds, timer));
    }

    IEnumerator VibrateDelay(long milliseconds, float timer)
    {
        yield return new WaitForSeconds(timer);
        Vibration.Vibrate(milliseconds);
    }
}
