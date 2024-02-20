using System.IO;
using System.Linq;
using GameConfiguration;
// using Palmmedia.ReportGenerator.Core.Common;
using Player;
using UnityEngine;

/// <summary>
/// Класс для первоначальной загрузки игры и настройки основных зависимостей
/// </summary>
public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private Player.Player _player;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Game _game;
    [SerializeField] private TextAsset _startCharacteristicsFile; 
    [SerializeField] private TextAsset _gameConfigFile;
    
    private PlayerStartInfo _playerStartInfo;
    private GameNodes _gameNodes;
    private MethodFromStringExecuter _executer;
    
    private void Awake()
    {
        _executer = new MethodFromStringExecuter(_game ,_player, _inventory);
        _playerStartInfo = LoadFromJson<PlayerStartInfo>(_startCharacteristicsFile);
        _gameNodes = LoadFromJson<GameNodes>(_gameConfigFile);           
    }

    private void Start()
    {
        _player.Init(_playerStartInfo.stats);
        _inventory.Init(_playerStartInfo.staff);
        _game.Init(_gameNodes.nodes);
    }

    /// <summary>
    /// Метод для парсинга JSON.
    /// </summary>
    /// <param name="textAsset">Текстовый ассет, который содержит JSON для парсинга в структуры данных</param>
    /// <typeparam name="T">Тип данных в который будут записаны данные из JSON.
    /// Про обобщенные типы данных можно прочитать по ссылке: https://metanit.com/sharp/tutorial/3.12.php </typeparam>
    /// <returns></returns>
    private T LoadFromJson<T>(TextAsset textAsset)
    {
        T toObject = default;
        
        if (textAsset)
        {
            toObject = JsonUtility.FromJson<T>(textAsset.text);
        }
        else
            Debug.LogError($"Cannot find asset!");

        return toObject;
    }
}