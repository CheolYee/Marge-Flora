using UnityEngine;

namespace _00._Work._02._Scripts.Buttons
{
    public class NullWeaponBtn : MonoBehaviour
    {
        [SerializeField] private GameObject nullWeaponUI;
        public void NullWeapon()
        {
            nullWeaponUI.gameObject.SetActive(false);
        }
    }
}