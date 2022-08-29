using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace TagHighlight
{
    public class HierarchyTagHighlighting
    {
        private static readonly string k_DataPath = "Assets/HierarchyNameHighlight/ScriptableObject/TagHighlightDataSO.asset";
        private static readonly string k_BackIcon = "Assets/HierarchyNameHighlight/icons/gradient_1x16.png";

        private static TagHighlightDataSO data;

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            LoadData();
            EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
        }

        private static void LoadData()
        {
            data = AssetDatabase.LoadAssetAtPath(k_DataPath, typeof(TagHighlightDataSO)) as TagHighlightDataSO;
        }

        private static void OnGUI(int instanceID, Rect selectionRect)
        {
            if (data is null)
                LoadData();

            // idからオブジェクトを取得
            var target = EditorUtility.InstanceIDToObject(instanceID);
            if (target is null)
                return;

            // オブジェクト名を取得
            string targetName = target.name;

            for (int i = 0; i < data.tags.Count; i++)
            {
                var tag = data.tags[i];
                if (targetName == null || targetName == "")
                    continue;

                // オブジェクト名が "指定された文字列" の場合ハイライト化
                if (targetName.StartsWith(tag.fastName) && targetName.EndsWith(tag.lastName))
                {
                    var pos = selectionRect;
                    pos.x = 0;
                    pos.xMin = EditorGUIUtility.singleLineHeight * 1.75F;
                    pos.xMax = selectionRect.xMax;

                    // 背景色を変更
                    using (new GUIColorScope(tag.backColor))
                    {
                        GUI.DrawTexture(pos, AssetDatabase.LoadAssetAtPath<Texture>(k_BackIcon));
                    }

                    // DrowIcon Top
                    var drawTopRect = selectionRect;
                    drawTopRect.xMin = pos.xMin;
                    drawTopRect.width = EditorGUIUtility.singleLineHeight;
                    GUI.Button(drawTopRect, EditorGUIUtility.Load("sv_icon_dot13_pix16_gizmo") as Texture2D, new GUIStyle());

                    // DrawName
                    var drawNameRect = selectionRect;
                    drawNameRect.xMin = drawTopRect.xMin + EditorGUIUtility.singleLineHeight;
                    GUI.Label(drawNameRect, targetName, new GUIStyle()
                    {
                        fontStyle = FontStyle.Bold,
                        normal = new GUIStyleState() { textColor = Color.white },
                        focused = new GUIStyleState() { textColor = Color.white },
                    });

                    // DrowIcon Buttom
                    var drawButtomRect = selectionRect;
                    drawButtomRect.xMin += drawButtomRect.width - EditorGUIUtility.singleLineHeight;
                    drawButtomRect.width = EditorGUIUtility.singleLineHeight;
                    GUI.Button(drawButtomRect, EditorGUIUtility.Load("sv_icon_dot13_pix16_gizmo") as Texture2D, new GUIStyle());

                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }

    public class GUIColorScope : IDisposable
    {
        private readonly UnityEngine.Color m_color;

        public GUIColorScope(Color color)
        {
            this.m_color = UnityEngine.GUI.color;
            UnityEngine.GUI.color = color;
        }

        public void Dispose()
        {
            UnityEngine.GUI.color = this.m_color;
        }
    }
}