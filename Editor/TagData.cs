using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TagHighlight
{
    [Serializable]
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
            var backColorRect = position;
            backColorRect.height = lineHeight;
            EditorGUI.PropertyField(backColorRect, backColor);

            // Name Tags
            var fastNameRect = position;
            fastNameRect.height = lineHeight;
            fastNameRect.y += lineHeight;
            fastNameRect.width -= position.width / 2 + 10;

            var lastNameRect = position;
            lastNameRect.height = lineHeight;
            lastNameRect.y += lineHeight;
            lastNameRect.width -= position.width / 2 + 10;
            lastNameRect.x += position.width / 2 + 10;

            using (new LabelWidthScope(85F))
            {
                EditorGUI.PropertyField(fastNameRect, fastName, new GUIContent("Top Tag"));
                EditorGUI.PropertyField(lastNameRect, lastName, new GUIContent("Bottom Tag"));
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
#endif
}