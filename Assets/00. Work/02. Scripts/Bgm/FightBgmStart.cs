using System;
using _00._Work._02._Scripts.Manager.SoundManager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _00._Work._02._Scripts.Bgm
{
    public class FightBgmStart : MonoBehaviour
    {
        private void Start()
        {
            SoundManager.Instance.PlayBgm(Random.Range(0, 2) == 1 ? "Fight1" : "Fight2");
        }
    }
}