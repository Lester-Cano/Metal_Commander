using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStart : MonoBehaviour
{
    [SerializeField] private Unit unit1, unit2;
    [SerializeField] private Collider2D unitInspector;
    [SerializeField] private Button button;

    void Start()
    {
        unit1 = GetComponentInParent<Unit>();
        button = GetComponentInChildren<Button>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ally"))
        {
            unit2 = other.GetComponent<Unit>();
        }
    }
    
    public void Combat()
    {
        Debug.Log(("te pego"));
        
        unit2.Attack(unit1);

        if (unit1.hitPoints > 0)
        {
            unit1.Attack(unit2);
        }
    }
}
