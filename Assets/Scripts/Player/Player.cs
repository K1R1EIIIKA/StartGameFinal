using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Player: MonoBehaviour
    {
        public List<int> _characteristics;

        [SerializeField] private string[] _characteristicNames;
        
        public UnityEvent<List<int>> CharacteristicsChanged;

        public int MaxHealth => _characteristics[3] * 20 > 0 ? _characteristics[3] * 20 : 0;
        public int Health { get; private set; }
        public int Exp { get; private set; }

        public void Init(List<int> characteristics)
        {
            _characteristics = characteristics;
            Health = MaxHealth;
            Exp = 0;
            CharacteristicsChanged?.Invoke(_characteristics);
        }
        
        public void ChangeHealth(int value)
        {
            Health += value;
            if (Health > MaxHealth)
                Health = MaxHealth;
            if (Health < 0)
                Health = 0;
            
            CharacteristicsChanged?.Invoke(_characteristics);
        }
        
        public void ChangeExp(int value)
        {
            Exp += value;
            
            CharacteristicsChanged?.Invoke(_characteristics);
        }
        
        public void ChangeCharacteristic(int id, int value)
        {
            if (id < 0 || id >= _characteristics.Count) 
                throw new System.ArgumentOutOfRangeException(nameof(id), "id out of range");
            
            _characteristics[id] += value;
            
            if (Health > MaxHealth)
                Health = MaxHealth;
            
            CharacteristicsChanged?.Invoke(_characteristics);
        }

        public bool IsStatMore(int id, int value)
        {
            return _characteristics[id] >= value;
        }
        
        public bool IsStatLess(int id, int value)
        {
            return _characteristics[id] < value;
        }
        
        public bool IsStatEqual(int id, int value)
        {
            return _characteristics[id] == value;
        }
    }
}