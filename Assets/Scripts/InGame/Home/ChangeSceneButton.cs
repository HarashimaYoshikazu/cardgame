using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ChangeSceneButton : MonoBehaviour
{
    Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener
            (() => { GameManager.Instance.GameCycle.GoBattle(); } );
    }
}
