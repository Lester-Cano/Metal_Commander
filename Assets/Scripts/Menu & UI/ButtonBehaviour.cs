using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private TurnSystem.TurnSystem _turnSystem;
    [SerializeField] private Animator animator;
    [SerializeField] private string sceneToLoad;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
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

    public void FadeToLevel(string name)
    {
        sceneToLoad = name;
        animator.SetTrigger("FadeOut");
    }

    public void FadeToCombat()
    {
        animator.SetTrigger("FadeToCombat");
    }
    
    public void FadeOutCombat()
    {
        animator.SetTrigger("FadeOutCombat");
    }
}
