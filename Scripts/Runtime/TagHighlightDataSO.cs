using System.Collections.Generic;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace TagHighlight
{
    [CreateAssetMenu(menuName = "TagHighlightData")]
    public class TagHighlightDataSO : ScriptableObject
    {
        public List<TagData> tags = new();
    }
}