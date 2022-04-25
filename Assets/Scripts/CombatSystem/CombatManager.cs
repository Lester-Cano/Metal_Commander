using System.Collections;
using UnityEngine;

namespace CombatSystem
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private GameObject combatStation;
        [SerializeField] private GameObject canvas;
        [SerializeField] private Camera mainCamera;


        public IEnumerator MoveToCombat(Unit unit, Unit unit2)
        {
            yield return new WaitForSeconds(1);
            
            mainCamera.transform.position = new Vector3(0, -39, -10);
            canvas.SetActive(false);
            combatStation.SetActive(true);
        }
    }
}
