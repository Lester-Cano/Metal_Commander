using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    #region Stats and Constructor
    [SerializeField] public string unitName, className, unitSide;
    [SerializeField] public int hitPoints, maxHP, movement, weaponPower, attack, defense;
    [SerializeField] public bool hasMoved, isDead, hasAttacked;
    [SerializeField] public AudioClip getHit, hit, selected, spaceSelected;
    [SerializeField] public Sprite portrait;

    [SerializeField] private GameObject parent;

    [SerializeField] public GameObject path;

    //UI for healthbar

    [SerializeField] public HealthBarBehaviour healthBarBehaviour;
    
    //From here audio System

    [SerializeField] private AudioManager source;
    
    //Animator

    [SerializeField] public Animator anim;
    
    [SerializeField] private TurnSystem.TurnSystem turnSystem;

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
        turnSystem = FindObjectOfType<TurnSystem.TurnSystem>();
    }

    private void Update()
    {
        if (isDead)
        {
            parent.SetActive(false);
        }
    }
    

    #region Combat Methods

    public IEnumerator Attack(Unit attacked)
    {
        anim.SetBool("Attack", true);
        
        yield return new WaitForSeconds(1f);

        source.Play("Hit");
        anim.SetBool("Attack", false);

        yield return new WaitForSeconds(0.2f);
        
        source.Play("GetHit");

        attacked.hitPoints -= weaponPower + attack - attacked.defense;

        if (attacked.hitPoints > 0)
        {
            attacked.anim.SetBool("Attack", true);

            yield return new WaitForSeconds(1f);
            
            attacked.source.Play("Hit");
            attacked.anim.SetBool("Attack", false);
            
            yield return new WaitForSeconds(0.2f);
        
            attacked.source.Play("GetHit");
            
            hitPoints -= attacked.weaponPower + attacked.attack - defense;
        }
        
        if (attacked.hitPoints <= 0)
        {
            attacked.anim.SetBool("Death", true);

            yield return new WaitForSeconds(1.2f);
            
            if (attacked.CompareTag("Ally"))
            {
                turnSystem.playerCount++;
            }
            if (attacked.CompareTag("Enemy"))
            {
                turnSystem.enemyCount++;
            }
            
            attacked.isDead = true;
        }

        if (hitPoints <= 0)
        {
            anim.SetBool("Death", true);

            yield return new WaitForSeconds(1.2f);
            
            if (CompareTag("Ally"))
            {
                turnSystem.playerCount++;
            }
            if (CompareTag("Enemy"))
            {
                turnSystem.enemyCount++;
            }
            
            isDead = true;
        }
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
