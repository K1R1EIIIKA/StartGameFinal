using System;
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
        Debug.Log(String.Join(", ", parameters));
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
    
    #endregion
}