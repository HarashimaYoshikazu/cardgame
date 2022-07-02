using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class ChangeSceneButton : MonoBehaviour
{
    Button _button;
    [SerializeField]
    string _sceneName = "Battle";
    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener
            (() => { SceneManager.LoadScene(_sceneName); } );
    }
}
