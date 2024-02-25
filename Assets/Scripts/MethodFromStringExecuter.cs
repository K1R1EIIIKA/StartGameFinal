using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

///<summary>
/// Обёртка для вызова методов с помощью строки и списка аргументов.
/// Служит для вызова функций исходя из данных которые приходят из конфигурационного файла JSON
/// </summary>
public class MethodFromStringExecuter
{
    public static MethodFromStringExecuter Instance { get; private set; }
    
    private Player.Player _player;
    private Inventory _inventory;
    private Game _game;
    
    public MethodFromStringExecuter(Game game, Player.Player player, Inventory inventory)
    {
        Instance = this;
        _player = player;
        _inventory = inventory;
        _game = game;
    }
    
    /// <summary>
    /// Вызывает метод с названием <c>name</c> и параметрами <c>parameters</c>
    /// </summary>
    /// <param name="name">Имя метода</param>
    /// <param name="parameters">Список параметров</param>
    public void InvokeMethod(string name, object[] parameters)
    {
        var method = GetType().GetMethod(name);
        if (method != null)
            method.Invoke(this,  parameters);
    }

    /// <summary>
    /// Вызывает метод с названием <c>name</c> и параметрами <c>parameters</c>.
    /// Возвращает флаг.
    /// </summary>
    /// <param name="name">Имя метода</param>
    /// <param name="parameters">Список параметров</param>
    /// <returns>Выполнено ли условие</returns>
    public bool InvokeConditionMethod(string name, object[] parameters)
    {
        // Debug.Log(String.Join(", ", parameters));
        var method = GetType().GetMethod(name);
        if (method != null)
            return (bool) method.Invoke(this,  parameters);
        return false;
    }

    #region Actions

    public void ChangeItemInInventoryAt(int id, int count) =>
        _inventory.ChangeItemInInventoryAt(id, count);
    
    public void ChangeCharacteristic(int id, int value) =>
        _player.ChangeCharacteristic(id, value);
    
    public void ChangeHealth(int value) =>
        _player.ChangeHealth(value);

    public void ChangeExp(int value) =>
        _player.ChangeExp(value);
    
    public void ClearItem(int id) =>
        _inventory.ChangeItemInInventoryAt(id, -_inventory.Items[id]);
    
    public void GoToDialogIfPowerAgility(int powerId, int powerValue, int agilityId, int agilityValue, int dialogId, int elseDialogId)
    {
        if (_player.IsStatMore(powerId, powerValue) && _player.IsStatMore(agilityId, agilityValue))
            _game.GoToDialog(dialogId);
        else
            _game.GoToDialog(elseDialogId);
    }
    
    public void GoToDialogIfHealthLuck(int healthValue, int luckValue, int dialogId, int elseDialogId)
    {
        if (_player.IsHealthMore(healthValue) && _player.IsStatMore(2, luckValue))
            _game.GoToDialog(dialogId);
        else
            _game.GoToDialog(elseDialogId);
    }
    
    public void Win() =>
        _game.Win();

    public void Lose() =>
        _game.Lose();
    
    #endregion
    
    #region Conditions
    
    public bool IsAnswered(int id, int shouldAnswer) =>
        _game.IsAnswered(id, shouldAnswer);
    
    public bool IsStatMore(int id, int value) =>
        _player.IsStatMore(id, value);
    
    public bool IsStatLess(int id, int value) =>
        _player.IsStatLess(id, value);
    
    public bool IsStatEqual(int id, int value) =>
        _player.IsStatEqual(id, value);
    
    public bool IsHealthMore(int value) =>
        _player.IsHealthMore(value);
    
    public bool IsHealthLess(int value) =>
        _player.IsHealthLess(value);
    
    public bool HasItem(int id, int count) =>
        _inventory.HasItem(id, count);
    
    public bool HasNoItem(int id, int count) =>
        _inventory.HasNoItem(id, count);
    
    #endregion
}