using System;
using System.Collections.Generic;
using _00._Work._02._Scripts.Marge.SO;
using UnityEngine;


[Serializable]

[CreateAssetMenu(fileName = "PoolingList", menuName = "SO/Pool/List", order = 0)]
public class PoolingListSO : ScriptableObject
{
    public List<PoolItem> items;
}