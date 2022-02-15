using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] string unitName;
    [SerializeField] string className;

    [SerializeField] public int hitPoints;
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int movement;

    //Generic weapon
    [SerializeField] int weaponPower = 5;

    [SerializeField] bool hasMoved;
    [SerializeField] bool isDead;

    [SerializeField] string unitSide;
    [SerializeField] SpriteRenderer unitSprite;
    [SerializeField] Animator Anim;

    [SerializeField] AudioClip getHit;
    [SerializeField] AudioClip hit;

    public Unit(int hitPoints, int maxHP, int attack, int defense, int movement, int weaponPower)
    {
        this.hitPoints = hitPoints;
        this.maxHP = maxHP;
        this.attack = attack;
        this.defense = defense;
        this.movement = movement;
        this.weaponPower = weaponPower;
    }

    public void Attack(Unit attacker, Unit attacked)
    {
        if (attacker.unitSide != attacked.unitSide)
        {
            attacked.hitPoints -= attacker.weaponPower + attacker.attack - attacked.defense;
        }
        else
        {
            Debug.Log("Can´t attack an ally");
            return;
        }
    }

    public void Heal(Unit healer, Unit healed)
    {
        if (healer.unitSide == healed.unitSide)
        {
            healed.hitPoints += healer.weaponPower + healer.attack - healer.attack / 3;

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
}
