using System;
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

            var i = 0;
            for (; i < _data.tags.Count; i++)
            {
                var tag = _data.tags[i];
                if (targetName == "")
                    continue;

                // オブジェクト名が "指定された文字列" の場合ハイライト化
                if (targetName.StartsWith(tag.fastName) && targetName.EndsWith(tag.lastName))
                {
                    var pos = selectionRect;
                    pos.x = 0;
                    pos.xMin = 31.5F;
                    pos.xMax = selectionRect.xMax;

                    // 背景色を変更
                    using (new GUIColorScope(tag.backColor))
                    {
                        GUI.DrawTexture(pos, AssetDatabase.LoadAssetAtPath<Texture>(BackIcon));
                    }

                    // DrowIcon Top
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

                    // DrowIcon Buttom
                    var drawButtomRect = selectionRect;
                    drawButtomRect.width = EditorGUIUtility.singleLineHeight;
                    drawButtomRect.x = selectionRect.xMax - drawButtomRect.width;
                    GUI.Button(drawButtomRect, EditorGUIUtility.Load("sv_icon_dot13_pix16_gizmo") as Texture2D, new GUIStyle());

                    break;
                }
            }   
        }

        
    }

    public class GUIColorScope : IDisposable
    {
        private readonly Color _mColor;

        public GUIColorScope(Color color)
        {
            _mColor = GUI.color;
            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = _mColor;
        }
    }
}