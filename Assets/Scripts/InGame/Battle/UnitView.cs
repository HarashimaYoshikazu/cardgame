using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class UnitView : MonoBehaviour, IDamage
{
    UnitType _unitType= UnitType.None;
    public void SetUnitType(UnitType unitType)
    {
        _unitType = unitType;
    }

    private void Start()
    {
        if (_unitType == UnitType.None)
        {
            throw new System.ArgumentNullException
                ("UnitTypeの値が不正です。" +
                "UnitView.Instantiate(UnitView prefab, Transform parent, UnitType unitType)のメソッドを使用して生成してください");
        }

        var image = GetComponent<UnityEngine.UI.Image>();
        //ひとまず色だけ変える。TODO UnitDataからSpriteを取ってくる
        switch (_unitType)
        {          
            case UnitType.Player:
                image.color = Color.red;
                break;
            case UnitType.Opponent:
                image.color = Color.white;
                break;
        }
    }

    public void Damage(int value)
    {
        switch (_unitType)
        {
            case UnitType.Player:
                BattleManager.Instance.Player.ChangeCurrentHP(value);
                break;
            case UnitType.Opponent:
                BattleManager.Instance.Enemy.ChangeCurrentHP(value);
                break;
        }
    }
    public static UnitView Instantiate(UnitView prefab, Transform parent, UnitType unitType)
    {
        Debug.Log(unitType);
        UnitView obj = Instantiate(prefab, parent);
        obj.SetUnitType(unitType);
        return obj;
    }
}
