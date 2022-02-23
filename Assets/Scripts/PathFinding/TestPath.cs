using System;
using System.Collections;
using System.Collections.Generic;
using PathFinding;
using UnityEngine;

public class TestPath : MonoBehaviour
{
    [SerializeField] private GameObject Objective;
    [SerializeField] private GameObject target;

    [SerializeField] private Pathfinding2D path;

    private void Update()
    {
        path.FindPath(Objective.transform.position, target.transform.position);
    }
}
