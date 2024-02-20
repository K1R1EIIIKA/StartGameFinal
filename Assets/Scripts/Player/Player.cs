using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Player: MonoBehaviour
    {
        public List<int> _characteristics;

        [SerializeField] private string[] _characteristicNames;

        public int MaxHealth => 200;
        public int Health { get; private set; }

        public void Init(List<int> characteristics)
        {
            _characteristics = characteristics;
            Health = MaxHealth;
        }
        
    }
}