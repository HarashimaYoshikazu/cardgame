using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;


public class CardGameEditerWindow : EditorWindow
{
    private CardBaseSO _sample;
    [MenuItem("Editor/CardGame")]
    private static void Create()
    {
        // 生成
        GetWindow<CardGameEditerWindow>("CardGameWindow");
    }

    private readonly string[] _tabToggles = { "TabA", "TabB", "TabC" };

    private int _tabIndex;

    private void OnGUI()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<CardBaseSO>();
        }

        //タブ
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            _tabIndex = GUILayout.Toolbar(_tabIndex, _tabToggles, new GUIStyle(EditorStyles.toolbarButton), GUI.ToolbarButtonSize.FitToContents);

        }
        EditorGUILayout.LabelField(_tabToggles[_tabIndex]);

        switch (_tabIndex)
        {
            case 0:
                using (new GUILayout.HorizontalScope())
                {
                    _sample.Name = EditorGUILayout.TextField("カードの名前", _sample.Name);
                }

                using (new GUILayout.HorizontalScope())
                {
                    _sample.SkillValue = EditorGUILayout.IntField("スキルの数", _sample.SkillValue);
                }

                using (new GUILayout.HorizontalScope())
                {
                    _sample.Element = (Elements)EditorGUILayout.EnumFlagsField("属性", _sample.Element);
                }

                using (new GUILayout.HorizontalScope())
                {
                    //_sample.Sprite = EditorGUILayout.ObjectField
                    _sample.Sprite = EditorGUILayout.ObjectField("Sprite", _sample.Sprite, typeof(Sprite), false) as Sprite;
                }
                break;
        }



    }


    private const string ASSET_PATH = "Assets/Resources/WindowCard.asset";
    private void Export()
    {
        // 新規の場合は作成
        if (!AssetDatabase.Contains(_sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(_sample, ASSET_PATH);
        }
        // インスペクターから設定できないようにする
        _sample.hideFlags = HideFlags.NotEditable;
        // 更新通知
        EditorUtility.SetDirty(_sample);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }

    private void Import()
    {
        CardBaseSO sample = AssetDatabase.LoadAssetAtPath<CardBaseSO>(ASSET_PATH);
        if (sample == null)
            return;

        _sample = sample;
    }
}
