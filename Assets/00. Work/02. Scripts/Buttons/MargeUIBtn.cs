using UnityEngine;

namespace _00._Work._02._Scripts.Buttons
{
    public class MargeUIBtn : MonoBehaviour
    {
        public void OpenMargeBoardUI()
        {
            UIContainer.Instance.storyUI.SetActive(false);
            UIContainer.Instance.characterImg.SetActive(false);
            UIContainer.Instance.characterUI.SetActive(false);
            UIContainer.Instance.dungeonUI.SetActive(false);
            UIContainer.Instance.margeBoardUI.SetActive(true);
        }
    }
}
