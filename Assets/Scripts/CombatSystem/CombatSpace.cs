using System;
using UnityEngine;
using TMPro;

namespace CombatSystem
{
    public class CombatSpace : MonoBehaviour
    {
        [SerializeField] public UnitCard unitCard1, unitCard2;
        
        [SerializeField] private TMP_Text unit1Name;
        [SerializeField] private TMP_Text unit1Hp;
        [SerializeField] private TMP_Text unit1Attack;
        [SerializeField] private TMP_Text unit1Defense;
        [SerializeField] private Animator unit1Anim;
        [SerializeField] private SpriteRenderer unit1Renderer;
        
        [SerializeField] private TMP_Text unit2Name;
        [SerializeField] private TMP_Text unit2Hp;
        [SerializeField] private TMP_Text unit2Attack;
        [SerializeField] private TMP_Text unit2Defense;
        [SerializeField] private Animator unit2Anim;
        [SerializeField] private SpriteRenderer unit2Renderer;

        private void Start()
        {
            unitCard1 = ScriptableObject.CreateInstance<UnitCard>();
            unitCard2 = ScriptableObject.CreateInstance<UnitCard>();
        }

        public void UpdateCardValue(Unit unit, Unit unit2)
        {
            unitCard1.unitName = unit.unitName;
            unitCard1.hp = unit.maxHP + "/" + unit.hitPoints;
            unitCard1.attack = (unit.attack + unit.weaponPower).ToString();
            unitCard1.defense = unit.defense.ToString();
            unitCard1.renderer = unit.GetComponentInParent<SpriteRenderer>();
            unitCard1.animator = unit.GetComponentInParent<Animator>();
            
            unitCard2.unitName = unit2.unitName;
            unitCard2.hp = unit2.maxHP + "/" + unit2.hitPoints;
            unitCard2.attack = (unit2.attack + unit2.weaponPower).ToString();
            unitCard2.defense = unit2.defense.ToString();
            unitCard2.renderer = unit2.GetComponentInParent<SpriteRenderer>();
            unitCard2.animator = unit2.GetComponentInParent<Animator>();
        }

        public void UpdateTitlesValue()
        {
            unit1Name.text = unitCard1.unitName;
            unit1Hp.text = unitCard1.hp;
            unit1Attack.text = unitCard1.attack;
            unit1Defense.text = unitCard1.defense;
            unit1Anim = unitCard1.animator;
            unit1Renderer = unitCard1.renderer;
            
            unit2Name.text = unitCard2.unitName;
            unit2Hp.text = unitCard2.hp;
            unit2Attack.text = unitCard2.attack;
            unit2Defense.text = unitCard2.defense;
            unit2Anim = unitCard2.animator;
            unit2Renderer = unitCard2.renderer;
        }
    }
}
