using System.Collections.Generic;
using UnityEngine;

namespace _00._Work._02._Scripts.Story.SO
{
    [CreateAssetMenu(fileName = "StoryContainer", menuName = "SO/Story/StoryContainer")]
    public class StoryContainerSo : ScriptableObject
    {
        public string storyId;
        public string storyName;
        public string storyDesc;
        public Sprite storyIcon;
        public List<StoryLineSo> storyLines;
        public List<StoryLineSo> choiceLines;
    }
}