using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private Unit unit1, unit2;
    [SerializeField] private Collider2D unitInspector;
    [SerializeField] private GameObject buttonContainer;

    [SerializeField] private TurnSystem.TurnSystem turnSystem;

    void Start()
    {
        unit1 = GetComponentInParent<Unit>();
        buttonContainer.SetActive(false);
        turnSystem = FindObjectOfType<TurnSystem.TurnSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ally"))
        {
            unit2 = other.GetComponent<Unit>();
            buttonContainer.SetActive(true);

            if (unit2.hasAttacked == true)
            {
                buttonContainer.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ally"))
        {
            buttonContainer.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        buttonContainer.SetActive(true);
    }

    public void Combat()
    {
        if (unit1 == null || unit2 == null)
        {
            return;
        }
        else
        {
            if (unit2.hasAttacked == false)
            {
                unit2.Attack(unit1);
                
                if (unit1.hitPoints > 0)
                {
                    unit1.Attack(unit2);
                }
                else
                {
                    unit1.isDead = true;
                    turnSystem.enemyCount++;
                }

                unit2.hasAttacked = true;
                
                buttonContainer.SetActive(false);
            }
            else
            {
                Debug.Log("Unit already attacked");
            }
        }
    }
}
