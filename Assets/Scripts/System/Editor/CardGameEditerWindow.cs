using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class CardGameEditerWindow : EditorWindow
{
    List<CardBaseSO> cards = new List<CardBaseSO>();
    private CardBaseSO _sample;
    [MenuItem("Editor/CardGame")]
    private static void Create()
    {
        // 生成
        GetWindow<CardGameEditerWindow>("CardGameWindow");
    }

    private readonly string[] _tabToggles = { "カード作成", "デッキ構築", "蛇足" };

    private int _tabIndex;

    private void OnGUI()
    {
        if (_sample == null)
        {
            // 読み込み
            Import();
        }

        //タブ
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            _tabIndex = GUILayout.Toolbar(_tabIndex, _tabToggles, new GUIStyle(EditorStyles.toolbarButton), GUI.ToolbarButtonSize.FitToContents);

        }

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

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("読み込み"))
                    {
                        Import();
                    }

                    if (GUILayout.Button("書き出し"))
                    {
                        Export();
                    }
                }

                break;
        }


    }

    private const string ASSET_PATH = "Assets/Resources/WindowCard.asset";
    private void Export()
    {
        // 読み込み
        CardBaseSO sample = AssetDatabase.LoadAssetAtPath<CardBaseSO>(ASSET_PATH);
        if (sample == null)
        {
            sample = ScriptableObject.CreateInstance<CardBaseSO>();
        }

        // 新規の場合は作成
        if (!AssetDatabase.Contains(sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // アセット作成
            AssetDatabase.CreateAsset(sample, ASSET_PATH);
        }

        // コピー
        //sample.Copy(_sample);
        EditorUtility.CopySerialized(_sample, sample);

        // 直接編集できないようにする
        sample.hideFlags = HideFlags.NotEditable;
        // 更新通知
        EditorUtility.SetDirty(sample);
        // 保存
        AssetDatabase.SaveAssets();
        // エディタを最新の状態にする
        AssetDatabase.Refresh();
    }

    private void Import()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<CardBaseSO>();
        }

        CardBaseSO sample = AssetDatabase.LoadAssetAtPath<CardBaseSO>(ASSET_PATH);
        if (sample == null)
            return;

        // コピーする
        //_sample.Copy(sample);
        EditorUtility.CopySerialized(sample, _sample);
    }
}
