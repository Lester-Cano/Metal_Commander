using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public string TileName;
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private bool _isWalkable;

    //public BaseUnit OccupiedUnit;
    //public bool Walkable => _isWalkable && OccupiedUnit == null;
    public void Init(bool isOffset) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    void OnMouseEnter() {
        _highlight.SetActive(true);
    }
    void OnMouseExit() {
        _highlight.SetActive(false);
    }

    //public void SetUnit(BaseUnit unit) {
    //    if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
    //    unit.transform.position = transform.position;
    //    OccupiedUnit = unit;
    //    unit.OccupiedTile = this;
    //}

    //void OnMouseDown() {
    //    if (GameManager.Instance.GameState != GameState.HeroesTurn) return;

    //    if (OccupiedUnit != null)
    //    {
    //        if (OccupiedUnit.Faction == Faction.Hero) UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit);
    //        else
    //        {
    //            if (UnitManager.Instance.SelectedHero != null)
    //            {
    //                //var enemy = (BaseEnemy)OccupiedUnit;
    //                ////Attack
    //                //Destroy(enemy.gameObject);
    //                //UnitManager.Instance.SetSelectedHero(null);
    //            }
    //        }
    //    }
    //    else {
    //        if (UnitManager.Instance.SelectedHero != null) {
    //            SetUnit(UnitManager.Instance.SelectedHero);
    //            UnitManager.Instance.SetSelectedHero(null);
    //        }
    //    }
    //}
}
