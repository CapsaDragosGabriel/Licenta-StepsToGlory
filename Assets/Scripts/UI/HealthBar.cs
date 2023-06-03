using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image heart;
    public TextMeshProUGUI textMeshPro;
    public void Start()
    {
        this.transform.localScale = Vector3.one;
        heart.transform.localScale = Vector3.one;
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        //slider.value = health;
        string healthString = health.ToString();
        healthString += "/" + slider.maxValue;
        textMeshPro.text = healthString;

        fill.color=gradient.Evaluate(1f);
    }
    public void SetHealth(float health)
    {
        slider.value = health;
        string healthString = health.ToString();
        healthString += "/" + slider.maxValue;
        textMeshPro.text = healthString;
        fill.color=gradient.Evaluate(slider.normalizedValue);
    }
    private void Update()
    {
        
    }
}
