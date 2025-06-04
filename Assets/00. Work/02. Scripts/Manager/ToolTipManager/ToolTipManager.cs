using _00._Work._02._Scripts.Character.Skills;
using TMPro;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.ToolTipManager
{
    public class ToolTipManager : MonoBehaviour
    {
        public static ToolTipManager Instance;
        
        [SerializeField] private GameObject tooltipObject;
        [SerializeField] private TextMeshProUGUI tooltipTitle;
        [SerializeField] private TextMeshProUGUI tooltipDescription;
        [SerializeField] private RectTransform tooltipRect;

        [SerializeField] private Vector2 offset;

        private void Start()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        private void PositionTooltip()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                tooltipRect.parent as RectTransform,
                Input.mousePosition,
                null,
                out Vector2 localPosition
            );

            tooltipRect.anchoredPosition = localPosition + offset;
        }

        public void ShowToolTip(SkillDataSo skillDataSo)
        {
            tooltipTitle.text = skillDataSo.skillName;
            tooltipDescription.text = skillDataSo.skillDescription;
            PositionTooltip();
            tooltipObject.SetActive(true);
        }
        
        public void ShowToolTip(PassiveDataSo passiveDataSo)
        {
            tooltipTitle.text = passiveDataSo.passiveSkillName;
            tooltipDescription.text = passiveDataSo.passiveDescription;
            PositionTooltip();
            tooltipObject.SetActive(true);
        }

        public void HideToolTip()
        {
            tooltipObject.SetActive(false);
        }
    }
}
