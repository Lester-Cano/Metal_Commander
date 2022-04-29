using System.Collections;
using UnityEngine;

namespace CombatSystem
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private Unit unit1, unit2;
        [SerializeField] private GameObject buttonContainer;
        [SerializeField] private float distance;

        void Start()
        {
            unit2 = GetComponentInParent<Unit>();
            buttonContainer.SetActive(false);
        }

        public void Combat()
        {
            if (unit2.hasAttacked == false)
            {
                //StartCoroutine(unit1.Attack(unit2));

                unit2.hasAttacked = true;
                buttonContainer.SetActive(false);
            }
        }

        private IEnumerator CallDistance(Unit unit1, Unit unit2)
        {
            yield return new WaitForSeconds(1f);
        
            distance = Vector3.Distance(unit1.transform.position, unit2.transform.position);
        
            if (distance <= 1)
            {
                buttonContainer.SetActive(true);
            }
        }
    }
}
