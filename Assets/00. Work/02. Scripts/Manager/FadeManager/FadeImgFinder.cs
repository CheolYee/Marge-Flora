using UnityEngine;
using UnityEngine.UI;

namespace _00._Work._02._Scripts.Manager.FadeManager
{
    public class FadeImgFinder : MonoBehaviour
    {
        private void Start()
        {
            FadeManager.Instance.fadeImage = gameObject.GetComponent<Image>();
            FadeManager.Instance.FadeOut();
        }
    }
}