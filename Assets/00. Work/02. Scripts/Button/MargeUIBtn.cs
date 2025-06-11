using System;
using UnityEngine;

namespace _00._Work._02._Scripts.Button
{
    public class MargeUIBtn : MonoBehaviour
    {
        public void OpenMargeBoardUI()
        {
            UIContainer.Instance.characterUI.SetActive(false);
            UIContainer.Instance.dungeonUI.SetActive(false);
            UIContainer.Instance.margeBoardUI.SetActive(true);
        }
    }
}
