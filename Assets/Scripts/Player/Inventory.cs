using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        public readonly List<int> Items = new List<int>();
        
        public UnityEvent<List<int>> InventoryInited;
        public UnityEvent<int, int> InventoryChanged;
    
        public void Init(List<int> staff)
        {
            print(staff.Count);
            staff.ForEach(o => Items.Add(o));
            InventoryInited?.Invoke(Items);
        }

        public void ChangeItemInInventoryAt(int id, int count)
        {
            if (id < 0 || id >= Items.Count) 
                throw new ArgumentOutOfRangeException(nameof(id), "id out of range");
            
            if (Items[id] + count < 0) 
                throw new ArgumentOutOfRangeException(nameof(count), "count out of range");
            
            Items[id] += count;
            
            InventoryChanged?.Invoke(id, Items[id]);
        }
    }
}
