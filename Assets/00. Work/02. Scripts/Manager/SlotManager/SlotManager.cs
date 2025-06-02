using System.Collections.Generic;
using _00._Work._02._Scripts.Marge.DragDrop;
using UnityEngine;

namespace _00._Work._02._Scripts.Manager.SlotManager
{
    public class SlotManager : MonoBehaviour
    {
        [SerializeField] public List<Slot> slots;

        // 가장 먼저 비어있는 슬롯을 반환
        public Slot GetEmptySlot()
        {
            foreach (var slot in slots)
            {
                if (!slot.HasItem()) return slot; //슬롯 차있는지 검사때리기
            }
            return null; // 모든 슬롯이 차있으면 null
        }
    }
}