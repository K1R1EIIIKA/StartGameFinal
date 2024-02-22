using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class StaffPanel : MonoBehaviour
    {
        [SerializeField] private Player.Player _player;
        
        [SerializeField] private Inventory _inventory;
        [SerializeField] private StaffElement _elementPrefab;
        [SerializeField] private Sprite[] _itemsImages;

        private List<StaffElement> _elements;

        private void Awake()
        {
            _inventory.InventoryInited.AddListener(Init);
            _inventory.InventoryChanged.AddListener(OnChangeInventory);
        }

        public void Init(List<int> staffStates)
        {
            _elements = new List<StaffElement>();
            Dictionary<int, Action> actions = new Dictionary<int, Action>
            {
                { 0, NoneAction },
                { 1, OnRabbitMeatClick },
                { 2, OnBearMeat },
                { 3, OnPigMeat },
                { 4, OnWolfTail },
                { 5, OnFoxTail },
                { 6, OnMushroom },
                { 7, NoneAction },
                { 8, OnGrass },
                { 9, NoneAction }
            };

            for (int i = 0; i < staffStates.Count; i++)
            {
                var newElement = Instantiate(_elementPrefab, transform);
                _elements.Add(newElement);
                InitElement(actions, newElement, staffStates, _elements.IndexOf(newElement));
            }
        }
        
        public void InitElement(Dictionary<int, Action> actions, StaffElement element, List<int> staffStates, int id)
        {
            if (actions.TryGetValue(id, out Action action))
            {
                element.Init(_itemsImages[id], action);
            }
            
            element.ChangeCollect(staffStates[id]);
        }

        private void OnChangeInventory(int id, int newValue)
        {
            _elements[id].ChangeCollect(newValue);
        }

        #region ActionsOnClick 

        public void NoneAction()
        {
            print("NoneAction");
        }
        
        public void OnRabbitMeatClick()
        {
            if (!_inventory.HasItem(1) || _player.Health == _player.MaxHealth)
                return;
            
            _player.ChangeHealth(5);
            _inventory.ChangeItemInInventoryAt(1, -1);
            
            GameLog.Instance.Log("[+5 ХП] Вы съели кроличье мясо"); 
        }
        
        public void OnBearMeat()
        {
            if (!_inventory.HasItem(2))
                return;
            
            _inventory.ChangeItemInInventoryAt(2, -1);
            _player.ChangeCharacteristic(0, 1);
            
            GameLog.Instance.Log("[+1 Сила] Вы съели медвежатину");
        }
        
        public void OnPigMeat()
        {
            if (!_inventory.HasItem(3))
                return;
            
            _inventory.ChangeItemInInventoryAt(3, -1);
            _player.ChangeCharacteristic(0, -1);
            
            GameLog.Instance.Log("[-1 Сила] Вы съели кабанятину");
        }
        
        public void OnWolfTail()
        {
            if (!_inventory.HasItem(4))
                return;
            
            _inventory.ChangeItemInInventoryAt(4, -1);
            _player.ChangeCharacteristic(1, -1);
            
            GameLog.Instance.Log("[-1 Ловкость] Вы съели хвост волка");
        }
        
        public void OnFoxTail()
        {
            if (!_inventory.HasItem(5))
                return;
            
            _inventory.ChangeItemInInventoryAt(5, -1);
            _player.ChangeCharacteristic(1, 1);
            
            GameLog.Instance.Log("[+1 Ловкость] Вы съели хвост лисы");
        }
        
        public void OnMushroom()
        {
            if (!_inventory.HasItem(6))
                return;
            
            _inventory.ChangeItemInInventoryAt(6, -1);
            int value = Random.Range(-1, 2);
            _player.ChangeCharacteristic(2, value);
            
            GameLog.Instance.Log($"[{(value >= 0 ? "+" : "")}{value} Удача] Вы съели гриб");

        }
        
        public void OnGrass()
        {
            print("OnGrass");
            if (!_inventory.HasItem(8))
                return;
            
            _inventory.ChangeItemInInventoryAt(8, -1);
            int value = Random.Range(-1, 2);
            _player.ChangeCharacteristic(3, value);
            
            GameLog.Instance.Log($"[{(value >= 0 ? "+" : "")}{value} Выносливость] Вы съели траву");
        }
        
        #endregion
    }
}