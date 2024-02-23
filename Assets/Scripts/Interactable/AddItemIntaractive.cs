using System;
using Player;
using UnityEngine;

namespace Interactable
{
    public class AddItemIntaractive : Interactive
    {
        [SerializeField] private int _itemId, _count, _exp;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Inventory _inventory;

        private void Awake()
        {
            _player = FindObjectOfType<Player.Player>();
            _inventory = FindObjectOfType<Inventory>();
        }

        protected override void DoAction()
        {
            _player.ChangeExp(_exp);
            _inventory.ChangeItemInInventoryAt(_itemId, _count);
            GameLog.Instance.Log($"[+1 {_inventory.ItemNames[_itemId]}] Вы нашли {_inventory.ItemNames[_itemId]}! ");
            
            gameObject.SetActive(false);
        }
    }
}