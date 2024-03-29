using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TagHighlight
{
    [CreateAssetMenu(menuName = "TagHighlightData")]
    public class TagHighlightDataSO : ScriptableObject
    {
        public List<TagData> tags = new List<TagData>();
    }
}