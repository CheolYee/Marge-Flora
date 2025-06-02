using UnityEngine;

namespace _00._Work._02._Scripts.Manager.Pooling
{
    public interface IPoolable
    {
        public string ItemName { get; }
        public GameObject GameObject { get; }
        public void ResetItem();
    }
}