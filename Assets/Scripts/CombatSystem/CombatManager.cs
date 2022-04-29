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
        [SerializeField] private Camera mainCamera;
        private Vector3 _prevPos;

        public IEnumerator MoveToCombat(Unit unit, Unit unit2)
        {
            yield return new WaitForSeconds(1);

            _prevPos = new Vector3(0, 0, -10) + unit.transform.position;
            mainCamera.transform.position = new Vector3(0, -39, -10);
            canvas.SetActive(false);
            combatStation.SetActive(true);
            
            SetScene(unit, unit2);
        }

        private void SetScene(Unit unit, Unit unit2)
        {
            combatSpace.UpdateCardValue(unit, unit2);
            combatSpace.UpdateTitlesValue();

            StartCoroutine(StartCombat(unit, unit2));
        }

        private IEnumerator StartCombat(Unit unit, Unit unit2)
        {
            yield return new WaitForSeconds(1);
            
            StartCoroutine(unit.Attack(unit2));
            
            unit.hasAttacked = true;
        }

        public IEnumerator BackToOverWorld(Unit unit, Unit unit2)
        {
            yield return new WaitForSeconds(1);
            
            mainCamera.transform.position = _prevPos;
            canvas.SetActive(true);
            combatStation.SetActive(false);
        }
    }
}
