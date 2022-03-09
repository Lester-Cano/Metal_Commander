using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{

    public Slider slider;
    public Vector3 offset;
    public Unit unit;

    public void SetHealth(float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
    }

    void Start()
    {
        unit = GetComponentInParent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {

        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        SetHealth(unit.hitPoints, unit.maxHP);

    }
}
