using _00._Work._02._Scripts.Marge;
using UnityEngine;

namespace _00._Work._02._Scripts.Buttons
{
    public class CharacterUIBtn : MonoBehaviour
    {
        public void OnClickCharUIBtn()
        {
            if (UIContainer.Instance.margeBoardUI.activeSelf)
            {
                MargeBoard margeBoard = UIContainer.Instance.margeBoardScript;

                margeBoard.SaveBoardState();
                margeBoard.LoadWeaponData();
            }

            UIContainer.Instance.storyUI.SetActive(false);
            UIContainer.Instance.dungeonUI.SetActive(false);
            UIContainer.Instance.margeBoardUI.SetActive(false);
            UIContainer.Instance.characterImg.SetActive(true);
            UIContainer.Instance.characterUI.SetActive(true);
        }
    }
}
