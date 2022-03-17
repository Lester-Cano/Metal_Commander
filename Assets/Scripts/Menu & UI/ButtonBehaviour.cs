using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private TurnSystem.TurnSystem _turnSystem;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowEnemyRange()
    {
        foreach (var r in _turnSystem.enemyTeam)
        {
            r.range.SetActive(true);
        }
    }
    
    public void HideEnemyRange()
    {
        foreach (var r in _turnSystem.enemyTeam)
        {
            r.range.SetActive(false);
        }
    }
}
