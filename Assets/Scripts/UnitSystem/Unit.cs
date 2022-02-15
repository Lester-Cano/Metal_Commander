using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Stats and Constructor
    [SerializeField] string unitName, className, unitSide;
    [SerializeField] public int hitPoints, maxHP, attack, defense, movement;
    [SerializeField] int weaponPower;
    [SerializeField] bool hasMoved, isDead;
    [SerializeField] AudioClip getHit, hit;

    public Unit(int hitPoints, int maxHP, int attack, int defense, int movement, int weaponPower)
    {
        this.hitPoints = hitPoints;
        this.maxHP = maxHP;
        this.attack = attack;
        this.defense = defense;
        this.movement = movement;
        this.weaponPower = weaponPower;
    }

    #endregion

    #region Combat Methods

    public void Attack(Unit attacked)
    {
        if (this.unitSide != attacked.unitSide)
        {
            attacked.hitPoints -= this.weaponPower + this.attack - attacked.defense;
        }
        else
        {
            Debug.Log("Can´t attack an ally");
            return;
        }
    }

    public void Heal(Unit healed)
    {
        if (this.unitSide == healed.unitSide)
        {
            healed.hitPoints += this.weaponPower + this.attack - this.attack / 3;

            if (healed.hitPoints > healed.maxHP)
            {
                healed.hitPoints = healed.maxHP;
            }
        }
        else
        {
            Debug.Log("Can´t heal an enemy");
            return;
        }
    }

    #endregion
}
