using UnityEngine;

namespace CombatSystem
{
    [CreateAssetMenu(fileName = "New UnitCard", menuName = "UnitCard")]
    public class UnitCard : ScriptableObject
    {
        public string unitName, hp, attack, defense;
        public Animator animator;
        public RuntimeAnimatorController controller;
    }
}
