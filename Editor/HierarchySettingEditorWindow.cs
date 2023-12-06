using TagHighlight;
using UnityEditor;

public class HierarchySettingEditorWindow : EditorWindow
{
    private TagHighlightDataSO _settingObject;
    private Editor _editor;
    
    [MenuItem("Tools/Open HierarchySettingEditorWindow")]
    private static void Open()
    {
        GetWindow<HierarchySettingEditorWindow>();
    }

    private void OnEnable()
    {
        _settingObject = TagHighlightDataSO.instance;
        _editor = Editor.CreateEditor(_settingObject);
    }

    private void OnGUI()
    {
        if (_settingObject == null) return;
        if (_editor == null) return;
        
        _editor.serializedObject.Update();
        _editor.OnInspectorGUI();
        _editor.serializedObject.ApplyModifiedProperties();
    }
}
