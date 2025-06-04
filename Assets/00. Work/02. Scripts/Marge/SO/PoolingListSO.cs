using System;
using System.Collections.Generic;
using UnityEngine;

namespace _00._Work._02._Scripts.Marge.SO
{
    [Serializable]

    [CreateAssetMenu(fileName = "PoolingList", menuName = "SO/Pool/List", order = 0)]
    public class PoolingListSO : ScriptableObject
    {
        public List<PoolItem> items;
    }
}