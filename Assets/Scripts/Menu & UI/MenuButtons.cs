using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] Transform _settingsP, _mainMenuP, _playMenuP, _newGameP, _continueP, _selectLvlP;
    void Start() {
        DOTween.Init();
    }

    public void OnPlayButton() {
        _camera.transform.DOMove(_playMenuP.position, 0.5f, false);
    }
    public void OnSettingsButton() {
        _camera.transform.DOMove(_settingsP.position, 0.5f, false);
    }
    public void OnMainBack() {
        _camera.transform.DOMove(_mainMenuP.position, 0.5f, false);
    }
    public void OnNewGameButton() {
        _camera.transform.DOMove(_newGameP.position, 0.5f, false);
    }
    public void OnNewGameBack() {
        _camera.transform.DOMove(_playMenuP.position, 0.5f, false);
    }
    public void OnContinueButton() {
        _camera.transform.DOMove(_continueP.position, 0.5f, false);
    }
    public void OnContinueBack() {
        _camera.transform.DOMove(_playMenuP.position, 0.5f, false);
    }
    public void OnSlotButton() {
        _camera.transform.DOMove(_selectLvlP.position, 0.5f, false);
    }
    public void OnLevelSelectBack() {
        _camera.transform.DOMove(_newGameP.position, 0.5f, false);
    }
}
