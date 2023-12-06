using System;
using UnityEditor;
using UnityEngine;

namespace TagHighlight
{
    public class LabelWidthScope : IDisposable
    {
        private readonly float _mLabelWidth;

        public LabelWidthScope(float labelWidth)
        {
            _mLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
        }

        public void Dispose()
        {
            EditorGUIUtility.labelWidth = _mLabelWidth;
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