using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CombatSystem
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private GameObject combatStation;
        [SerializeField] private CombatSpace combatSpace;
        [SerializeField] private GameObject canvas;
        [SerializeField] private GameObject button;
        [SerializeField] private Camera mainCamera;
        private Vector3 _prevPos;
        private static readonly int Attack = Animator.StringToHash("Attack");

        public IEnumerator MoveToCombat(Unit unit, Unit unit2)
        {
            yield return new WaitForSeconds(1);

            button.SetActive(false);
            _prevPos = new Vector3(0, 0, -10) + unit2.transform.position;
            mainCamera.transform.position = new Vector3(0, -39, -10);
            mainCamera.orthographicSize = 5.5f;
            canvas.SetActive(false);
            combatStation.SetActive(true);
            
            SetScene(unit, unit2);
        }

        private void SetScene(Unit unit, Unit unit2)
        {
            combatSpace.UpdateCardValue(unit, unit2);
            combatSpace.UpdateTitlesValue();

            if (unit.unitSide == "Enemy" || unit2.unitSide == "Enemy")
            {
                StartCoroutine(StartCombat(unit, unit2));
            }
            else
            {
                StartCoroutine(StartHeal(unit, unit2));
            }
        }

        private IEnumerator StartCombat(Unit unit, Unit unit2)
        {
            combatSpace.unit1Anim.SetBool(Attack, true);

            unit.Attack(unit2);
            unit.hasAttacked = true;

            yield return new WaitForSeconds(2);
            
            combatSpace.unit2Anim.SetBool(Attack, true);

            yield return new WaitForSeconds(1);
            
            combatSpace.UpdateCardValue(unit, unit2);
            combatSpace.UpdateTitlesValue();
            
            StartCoroutine(BackToOverWorld(unit, unit2));
        }
        
        private IEnumerator StartHeal(Unit unit, Unit unit2)
        {
            combatSpace.unit1Anim.SetBool(Attack, true);

            yield return new WaitForSeconds(1);

            unit.Heal(unit2);
            unit.hasAttacked = true;
            
            yield return new WaitForSeconds(1);
            
            combatSpace.UpdateCardValue(unit, unit2);
            combatSpace.UpdateTitlesValue();
            
            StartCoroutine(BackToOverWorld(unit, unit2));
        }

        private IEnumerator BackToOverWorld(Unit unit, Unit unit2)
        {
            yield return new WaitForSeconds(1);
            
            mainCamera.transform.position = _prevPos;
            mainCamera.orthographicSize = 3.5f;
            canvas.SetActive(true);
            combatStation.SetActive(false);
            button.SetActive(false);
        }
    }
}
