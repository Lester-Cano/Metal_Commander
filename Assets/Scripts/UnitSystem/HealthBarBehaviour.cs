using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{

    public Slider slider;
    public Vector3 offset;
    public Unit unit;

    private void Start()
    {
        unit = GetComponentInParent<Unit>();
    }
    
    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);
        SetHealth(unit.hitPoints, unit.maxHP);
    }
    
    public void SetHealth(float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
    }
}
