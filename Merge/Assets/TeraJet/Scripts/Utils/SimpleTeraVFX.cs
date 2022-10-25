using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace TeraJet
{
    public class SimpleTeraVFX
    {
        public static void PlayGotHitFX(GameObject gameObject, Color oldColor, float blinkIntensity = 10f, float blinkDuration = 0.1f)
        {
            
            ChangeMaterial(gameObject, oldColor, blinkIntensity, blinkDuration);
        }

        public static async void ChangeMaterial(GameObject gameObject, Color oldColor, float blinkIntensity, float blinkDuration)
        {
            float blinkTimer = blinkDuration;
            do
            {
                blinkTimer -= Time.deltaTime;
                float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
                float intensity = lerp * blinkIntensity;
                gameObject.GetComponent<Renderer>().material.color = Color.white * intensity;
                await Task.Delay((int)(Time.deltaTime * 1000f));
            } while (gameObject.GetComponent<Renderer>().material.color.a >= 1f);
            gameObject.GetComponent<Renderer>().material.color = oldColor;
        }

    }
}

