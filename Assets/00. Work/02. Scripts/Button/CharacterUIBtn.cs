using _00._Work._02._Scripts.Marge;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Button
{
    public class CharacterUIBtn : MonoBehaviour
    {
        public void OnClickCharUIBtn()
        {
            if (UIContainer.Instance.margeBoardUI.activeSelf)
            {
                MargeBoard margeBoard = UIContainer.Instance.margeBoardUI.GetComponent<MargeBoard>();

                margeBoard.SaveBoardState();
            }

            UIContainer.Instance.characterUI.SetActive(true);
            UIContainer.Instance.dungeonUI.SetActive(false);
            UIContainer.Instance.margeBoardUI.SetActive(false);
        }
    }
}
