using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TagHighlight
{
    public static class HierarchyTagHighlighting
    {
        private const string BackIcon = "Assets/HierarchyNameHighlight/icons/gradient_1x16.png";

        private static TagHighlightDataSO _data;

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            LoadData();
            EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
        }

        private static void LoadData()
        {
            _data = TagHighlightDataSO.instance;
        }

        private static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (_data == null)
                LoadData();

            // idからオブジェクトを取得
            var target = EditorUtility.InstanceIDToObject(instanceID);
            if (target is null)
                return;

            // オブジェクト名を取得
            var targetName = target.name;
            if (string.IsNullOrEmpty(targetName))
                return;

            // マッチするタグがあるか検索
            var matchTag = _data.tags
                .FirstOrDefault(tag => targetName.EndsWith(tag.lastName) && targetName.StartsWith(tag.fastName));

            if (matchTag == null)
                return;

            // 名前からタグを除外
            targetName = targetName
                .Replace(matchTag.fastName, "")
                .Replace(matchTag.lastName, "");

            // オブジェクト名が "指定された文字列" の場合ハイライト化
            var pos = selectionRect;
            pos.x = 0;
            pos.xMin = 31.5F;
            pos.xMax = selectionRect.xMax;

            // 背景色を変更
            using (new GUIColorScope(matchTag.backColor))
            {
                GUI.DrawTexture(pos, AssetDatabase.LoadAssetAtPath<Texture>(BackIcon));
            }

            // DrawIcon Top
            var drawTopRect = selectionRect;
            drawTopRect.xMin = pos.xMin;
            drawTopRect.width = EditorGUIUtility.singleLineHeight;
            GUI.Button(drawTopRect, EditorGUIUtility.Load("sv_icon_dot13_pix16_gizmo") as Texture2D, new GUIStyle());

            // DrawName
            var drawNameRect = selectionRect;
            drawNameRect.xMin = drawTopRect.xMin;
            GUI.Label(drawNameRect, targetName, new GUIStyle
            {
                fontStyle = FontStyle.Bold,
                normal = new GUIStyleState { textColor = Color.white },
                focused = new GUIStyleState { textColor = Color.white },
                alignment = TextAnchor.MiddleCenter,
            });

            // DrawIcon Bottom
            var drawBottomRect = selectionRect;
            drawBottomRect.width = EditorGUIUtility.singleLineHeight;
            drawBottomRect.x = selectionRect.xMax - drawBottomRect.width;
            GUI.Button(drawBottomRect, EditorGUIUtility.Load("sv_icon_dot13_pix16_gizmo") as Texture2D, new GUIStyle());
        }
    }
}