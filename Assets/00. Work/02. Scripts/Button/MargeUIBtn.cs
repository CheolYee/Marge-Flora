using System;
using UnityEngine;

namespace _00._Work._02._Scripts.Button
{
    public class MargeUIBtn : MonoBehaviour
    {
        public GameObject margeBoardUI;
        
        public GameObject characterUI;

        
        public void OpenMargeBoardUI()
        {
            characterUI.SetActive(false);
            margeBoardUI.SetActive(true);
        }
    }
}
