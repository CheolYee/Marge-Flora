using System.Collections.Generic;
using UnityEngine;

namespace _00._Work._02._Scripts.Story.SO
{
    [CreateAssetMenu(fileName = "StoryContainer", menuName = "SO/Story/StoryContainer")]
    public class StoryContainerSo : ScriptableObject
    {
        public string storyId;
        public List<StoryLineSo> storyLines;
    }
}