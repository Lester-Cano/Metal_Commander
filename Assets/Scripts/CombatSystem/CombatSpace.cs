using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CombatSystem
{
    public class CombatSpace : MonoBehaviour
    {
        [SerializeField] public UnitCard unitCard1, unitCard2;
        
        [SerializeField] private TMP_Text unit1Name;
        [SerializeField] private TMP_Text unit1Hp;
        [SerializeField] private TMP_Text unit1Attack;
        [SerializeField] private TMP_Text unit1Defense;
        public Animator unit1Anim;
        //[SerializeField] public RuntimeAnimatorController unit1Controller;

        [SerializeField] private TMP_Text unit2Name;
        [SerializeField] private TMP_Text unit2Hp;
        [SerializeField] private TMP_Text unit2Attack;
        [SerializeField] private TMP_Text unit2Defense;
        public Animator unit2Anim;
        //[SerializeField] public RuntimeAnimatorController unit2Controller;

        [SerializeField] private Image name, name2, hud, hud2;

        private void Start()
        {
            unitCard1 = ScriptableObject.CreateInstance<UnitCard>();
            unitCard2 = ScriptableObject.CreateInstance<UnitCard>();
        }

        public void UpdateCardValue(Unit unit, Unit unit2)
        {
            unitCard1.unitName = unit.unitName;
            unitCard1.hp = unit.maxHP.ToString() + "/" + unit.hitPoints.ToString();
            unitCard1.attack = (unit.attack + unit.weaponPower).ToString();
            unitCard1.defense = unit.defense.ToString();
            // unitCard1.animator = unit.anim;
            // unitCard1.controller = unit.controller;
            
            if (unit.unitSide == "Enemy")
            {
                name.color = new Color(1, 0.66f, 0.66f, 1);
                hud.color = new Color(1, 0.66f, 0.66f, 1);
            }
            else
            {
                name.color = new Color(0.27f, 0.78f, 1, 1);
                hud.color = new Color(0.27f, 0.78f, 1, 1);
            }

            unitCard2.unitName = unit2.unitName;
            unitCard2.hp = unit2.maxHP.ToString() + "/" + unit2.hitPoints.ToString();
            unitCard2.attack = (unit2.attack + unit2.weaponPower).ToString();
            unitCard2.defense = unit2.defense.ToString();
            // unitCard2.animator = unit2.anim;
            // unitCard2.controller = unit2.controller;
            
            if (unit2.unitSide == "Enemy")
            {
                name2.color = new Color(1, 0.66f, 0.66f, 1);
                hud2.color = new Color(1, 0.66f, 0.66f, 1);;
            }
            else
            {
                name2.color = new Color(0.27f, 0.78f, 1, 1);
                hud2.color = new Color(0.27f, 0.78f, 1, 1);
            }
        }

        public void UpdateTitlesValue()
        {
            unit1Name.text = unitCard1.unitName;
            unit1Hp.text = unitCard1.hp;
            unit1Attack.text = unitCard1.attack;
            unit1Defense.text = unitCard1.defense;
            //unit1Anim = unitCard1.animator;
            // unit1Controller = unitCard1.controller;
            // unit1Anim.runtimeAnimatorController = unit1Controller;

            unit2Name.text = unitCard2.unitName;
            unit2Hp.text = unitCard2.hp;
            unit2Attack.text = unitCard2.attack;
            unit2Defense.text = unitCard2.defense;
            //unit2Anim = unitCard2.animator;
            // unit2Controller = unitCard2.controller;
            // unit2Anim.runtimeAnimatorController = unit2Controller;
        }
    }
}
