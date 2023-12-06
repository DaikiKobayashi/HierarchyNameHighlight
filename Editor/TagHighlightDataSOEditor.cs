using TagHighlight;
using UnityEditor;

// ReSharper disable InconsistentNaming

[CustomEditor(typeof(TagHighlightDataSO))]
public class TagHighlightDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        serializedObject.ApplyModifiedProperties();
    }
}
