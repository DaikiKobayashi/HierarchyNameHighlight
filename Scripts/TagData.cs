using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            // Back Color
            Rect backColor_rect = position;
            backColor_rect.height = lineHeight;
            var backColor = property.FindPropertyRelative("backColor");
            EditorGUI.PropertyField(backColor_rect, backColor);

            // Label
            Rect highlightLabel_rect = backColor_rect;
            highlightLabel_rect.y += lineHeight;
            EditorGUI.LabelField(highlightLabel_rect, new GUIContent("Highlight Tags"));

            // Name Tags
            Rect fastName_rect = highlightLabel_rect;
            fastName_rect.y += lineHeight;
            fastName_rect.width /= 2;
            Rect lastName_rect = fastName_rect;
            lastName_rect.x += fastName_rect.width;

            var fastName = property.FindPropertyRelative("fastName");
            var lastName = property.FindPropertyRelative("lastName");
            EditorGUI.PropertyField(fastName_rect, fastName);
            EditorGUI.PropertyField(lastName_rect, lastName);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3;
        }
    }
#endif
}