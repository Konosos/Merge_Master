using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image image;

    public void SetColor(bool _isRed)
    {
        if(_isRed)
        {
            image.color = Color.red;
        }
        else
        {
            image.color = Color.green;
        }
    }

    public void SetHealth(int _health)
    {
        slider.value = _health;
    }
    public void SetMaxHealth(int _maxHealth)
    {
        slider.maxValue = _maxHealth;
        slider.value = _maxHealth;
    }
}
