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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ally"))
        {
            unit2 = other.GetComponent<Unit>();

            if (unit2.className == "Healer")
            {
                buttonContainer.SetActive(true);

                if (unit2.hasAttacked == true)
                {
                    buttonContainer.SetActive(false);
                }
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

    public void Combat()
    {
        if (unit2.hasAttacked == false)
        {
            StartCoroutine(unit2.Heal(unit1));

            unit2.hasAttacked = true;
            buttonContainer.SetActive(false);
        }
    }
}
