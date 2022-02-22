using System;
using UnityEngine;

namespace UnitSystem
{
    public class PathMovement : MonoBehaviour
    {
        [SerializeField] private Pathfinding2D _pathfinding2D;
        [SerializeField] public GameObject seeker;
        [SerializeField] public GameObject taget;
        private Vector3 worldPosition;

        private void Start()
        {
            worldPosition = new Vector3(0,0,0);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            
            _pathfinding2D.FindPath(seeker.transform.position, worldPosition);
        }
    }
}
