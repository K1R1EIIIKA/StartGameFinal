using TMPro;
using UnityEngine;

public class GameLog : MonoBehaviour
{ 
    public static GameLog Instance { get; private set; } //Сделать одиночку

    [SerializeField] private TMP_Text _logText;

    private void Awake()
    {
        Instance = this; // Иницилизация одиночки
        _logText.text = string.Empty; // инициализация текста лога
    }
    
    public void Log(string text)
    {
        _logText.text += $"-> {text} \n";
    }
    
    public void Log(string text, object[] parameters)
    {
        _logText.text += $"-> {text}: {string.Join(", ", parameters)} \n";
    }
    
    public void ClearLog()
    {
        _logText.text = string.Empty; 
    }
}