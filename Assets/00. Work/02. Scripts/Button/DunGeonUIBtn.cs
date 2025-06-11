using _00._Work._02._Scripts.Manager.SaveManager;
using _00._Work._02._Scripts.Marge;
using UnityEngine;

namespace _00._Work._02._Scripts.Button
{
    public class DunGeonUIBtn : MonoBehaviour
    {
        public void OnClickDungeonUIBtn()
        {
            if (UIContainer.Instance.margeBoardUI.activeSelf)
            {
                MargeBoard board = UIContainer.Instance.margeBoardUI.gameObject.GetComponent<MargeBoard>();
                
                board.SaveBoardState();
            }
            UIContainer.Instance.dungeonUI.SetActive(true);
            UIContainer.Instance.characterUI.SetActive(false);
            UIContainer.Instance.margeBoardUI.SetActive(false);
        }
    }
}
