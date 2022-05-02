using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealSystem : MonoBehaviour
{
    [SerializeField] private Unit unit1, unit2;
    [SerializeField] private GameObject buttonContainer;

    void Start()
    {
        unit1 = GetComponentInParent<Unit>();
        buttonContainer.SetActive(false);
    }

    public void Combat()
    {
        if (unit2.hasAttacked == false)
        {
            unit2.Heal(unit1);

            unit2.hasAttacked = true;
            buttonContainer.SetActive(false);
        }
    }
}
