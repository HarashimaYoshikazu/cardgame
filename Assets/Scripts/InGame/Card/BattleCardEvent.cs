using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardEvent : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler
{
    /// <summary>キャッシュ用の変数</summary>
    GameObject _currentPointerObject = null;
    BattleCard _battleCard = null;
    /*
    以下EventSystemsのインターフェイスの関数
    */
    private void Awake()
    {
        if (TryGetComponent(out BattleCard battleCard))
        {
            _battleCard = battleCard;
        }
        else
        {
            Debug.LogWarning("BattleCardコンポーネントが見つかりません。");
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _battleCard.OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //現在ポインター上にあるオブジェクトを検知して代入
        _currentPointerObject = eventData.pointerCurrentRaycast.gameObject;
        _battleCard.OnDrag(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _battleCard.OnPointerUp(_currentPointerObject);
    }
}
