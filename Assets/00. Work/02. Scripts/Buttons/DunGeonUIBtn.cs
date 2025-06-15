using _00._Work._02._Scripts.Marge;
using UnityEngine;

namespace _00._Work._02._Scripts.Buttons
{
    public class DunGeonUIBtn : MonoBehaviour
    {
        public void OnClickDungeonUIBtn()
        {
            if (UIContainer.Instance.margeBoardUI.activeSelf)
            {
                MargeBoard board = UIContainer.Instance.margeBoardScript;
                
                board.SaveBoardState();
                board.LoadWeaponData();
            }
            UIContainer.Instance.dungeonUI.SetActive(true);
            UIContainer.Instance.characterUI.SetActive(false);
            UIContainer.Instance.margeBoardUI.SetActive(false);
        }
    }
}
