using _00._Work._02._Scripts.Marge;
using _00._Work._08._Utility;
using UnityEngine;

namespace _00._Work._02._Scripts.Button
{
    public class CharacterUIBtn : MonoBehaviour
    {
        [SerializeField] private MargeBoard margeBoard;
        
        [SerializeField] private GameObject characterUI;
        
        public void OnClickCharUIBtn()
        {
            if (margeBoard.gameObject.activeSelf)
            {
                margeBoard.SaveBoardState();
                characterUI.SetActive(true);
                margeBoard.gameObject.SetActive(false);
            }
            else
            {
                characterUI.SetActive(true);
                Logging.Log("현재 조합 창이 아니어서 저장을 할 수 없습니다.");
            }
        }
    }
}
