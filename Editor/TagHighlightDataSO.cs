using System.Collections.Generic;
using UnityEditor;

// ReSharper disable InconsistentNaming

namespace TagHighlight
{
    [FilePath("ProjectSettings/HierarchyTagHighlightSetting.asset", FilePathAttribute.Location.ProjectFolder)]
    public class TagHighlightDataSO : ScriptableSingleton<TagHighlightDataSO>
    {
        public List<TagData> tags = new();
    }
}