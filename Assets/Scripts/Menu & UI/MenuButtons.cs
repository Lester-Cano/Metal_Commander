using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private Transform settingsP, mainMenuP, playMenuP, newGameP, selectLvlP;
    
    void Start() 
    {
        DOTween.Init();
    }

    public void OnPlayButton() 
    {
        camera.transform.DOMove(playMenuP.position, 0.5f, false);
    }
    public void OnSettingsButton() 
    {
        camera.transform.DOMove(settingsP.position, 0.5f, false);
    }
    public void OnMainBack() 
    {
        camera.transform.DOMove(mainMenuP.position, 0.5f, false);
    }
    
    public void OnLevelSelector() 
    {
        camera.transform.DOMove(selectLvlP.position, 0.5f, false);
    }
    public void OnNewGameButton()
    {
        camera.transform.DOMove(newGameP.position, 0.5f, false);
    }

    public void OnLevelSelectBack() 
    {
        camera.transform.DOMove( playMenuP.position, 0.5f, false);
    }
}
