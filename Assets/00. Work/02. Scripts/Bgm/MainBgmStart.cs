using _00._Work._02._Scripts.Manager.SoundManager;
using UnityEngine;

namespace _00._Work._02._Scripts.Bgm
{
    public class MainBgmStart : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.Instance.PlayBgm("Main");
        }
    }
}
