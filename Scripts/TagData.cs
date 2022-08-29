using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TagHighlight
{
    [System.Serializable]
    public class TagData
    {
        public Color backColor = Color.black;
        public string fastName;
        public string lastName;
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TagData))]
    public class TagDataEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var lineHeight = EditorGUIUtility.singleLineHeight;
            
            var backColor = property.FindPropertyRelative("backColor");
            var fastName = property.FindPropertyRelative("fastName");
            var lastName = property.FindPropertyRelative("lastName");

            // Back Color
            Rect backColor_rect = position;
            backColor_rect.height = lineHeight;
            EditorGUI.PropertyField(backColor_rect, backColor);

            // Name Tags
            Rect fastName_rect = position;
            fastName_rect.height = lineHeight;
            fastName_rect.y += lineHeight;
            fastName_rect.width -= position.width / 2 + 10;

            Rect lastName_rect = position;
            lastName_rect.height = lineHeight;
            lastName_rect.y += lineHeight;
            lastName_rect.width -= position.width / 2 + 10;
            lastName_rect.x += position.width / 2 + 10;

            using (new LabelWidthScope(85F))
            {
                EditorGUI.PropertyField(fastName_rect, fastName, new GUIContent("Top Tag"));
                EditorGUI.PropertyField(lastName_rect, lastName, new GUIContent("Buttom Tag"));
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
#endif

    public class LabelWidthScope : IDisposable
    {
        float m_labelWidth = 0;

        public LabelWidthScope(float labelWidth)
        {
            m_labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = m_labelWidth;
        }
    }
}