using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Stats and Constructor
    [SerializeField] public string unitName, className, unitSide;
    [SerializeField] public int hitPoints, maxHP, movement, weaponPower, attack, defense;
    [SerializeField] public bool hasMoved, isDead, hasAttacked;
    [SerializeField] public AudioClip getHit, hit;

    [SerializeField] private GameObject parent;

    [SerializeField] public GameObject path;

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

    private void Update()
    {
        if (isDead)
        {
            parent.SetActive(false);
        }
    }
    

    #region Combat Methods

    public void Attack(Unit attacked)
    {
        if (this.unitSide != attacked.unitSide)
        {
            attacked.hitPoints -= this.weaponPower + this.attack - attacked.defense;
        }
        else
        {
            Debug.Log("Can�t attack an ally");
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
            Debug.Log("Can�t heal an enemy");
            return;
        }
    }
    #endregion
}
