using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class CardGameEditerWindow : EditorWindow
{
    DefaultAsset _TargetFolder;
    private CardBaseSO _sample;
    [MenuItem("Editor/CardGame")]
    private static void Create()
    {
        // 生成
        GetWindow<CardGameEditerWindow>("CardGameWindow");
    }
    private void OnGUI()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<CardBaseSO>();
        }

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
            _sample.Element = (Elements)EditorGUILayout.EnumFlagsField("属性",_sample.Element);
        }

        using (new GUILayout.HorizontalScope())
        {
            //_sample.Sprite = EditorGUILayout.ObjectField
            _sample.Sprite = EditorGUILayout.ObjectField("Sprite",_sample.Sprite, typeof(Sprite), false) as Sprite;
        }

        using (new GUILayout.HorizontalScope())
        {
            // 書き込みボタン
            if (GUILayout.Button("書き込み"))
            {
                Export();
            }
            if (GUILayout.Button("読み込み"))
            {
                Import();
            }
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
