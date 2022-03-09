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
    [SerializeField] public AudioClip getHit, hit, selected, spaceSelected;

    [SerializeField] private GameObject parent;

    [SerializeField] public GameObject path;

    //UI for healthbar

    [SerializeField] public HealthBarBehaviour healthBarBehaviour;
    
    //From here audio System

    [SerializeField] private AudioManager source;

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

    private void Start()
    {
        source = FindObjectOfType<AudioManager>();
    }

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
        source.Play("Hit");
        source.Play("GetHit");

        attacked.hitPoints -= weaponPower + attack - attacked.defense;
    }

    public void Heal(Unit healed)
    {
        healed.hitPoints += weaponPower + attack - attack / 3;
        
        if (healed.hitPoints > healed.maxHP)
        {
            healed.hitPoints = healed.maxHP;
        }
    }
    #endregion
}
