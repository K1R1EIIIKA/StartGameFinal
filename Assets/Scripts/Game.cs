using System.Collections.Generic;
using System.Linq;
using GameConfiguration;
using Player;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    
    [SerializeField] private DialogPanel _dialogPanel;
    [SerializeField] private LocationBuilder _locationBuilder;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _expText;
    [SerializeField] private GameObject _pausePanel;
    
    private List<GameNode> _gameNodes;
    private readonly List<int> _savedAnswersId = new List<int>();
    private GameNode _currenGameNode;
    private DialogNode _currenDialogNode;

    private void Awake()
    {
        Instance = this;
    }

    public void Init(List<GameNode> nodes)
    {
        _gameNodes = nodes;
        StartGame();
    }

    public void StartGame()
    {
        GoToDialog(0);
    }

    public void GoToDialog(int id)
    {
        var gameNode = _gameNodes.FirstOrDefault(n => 
            n.dialogs.FirstOrDefault(d => d.id == id) != null);
        if(gameNode == null) return;
        
        if(gameNode != _currenGameNode)
            GoToLocation(gameNode.id);
        
        _currenDialogNode = _currenGameNode.dialogs.FirstOrDefault(d => d.id == id);
        _dialogPanel.Init(_currenDialogNode);
    }

    public void GoToLocation(int id)
    {
        _currenGameNode = _gameNodes.FirstOrDefault(n => n.id == id);
        _locationBuilder.Build(_currenGameNode.location);
    }

    public void SaveAnswer(int id)
    {
        _savedAnswersId.Add(id);
    }
    
    public bool IsAnswered(int id, int shouldAnswer)
    {
        // Debug.Log(_savedAnswersId.Contains(id) + " " + (shouldAnswer == 1));
        return _savedAnswersId.Contains(id) == (shouldAnswer == 1);
    }
    
    
    public List<int> GetSavedAnswers()
    {
        return _savedAnswersId;
    }

    public void Win()
    {
        GameLog.Instance.Log("[Вы выиграли!]");
        _savedAnswersId.Clear();
        
        _goldText.text = FindObjectOfType<Inventory>().Items[0] + " золота";
        _expText.text = FindObjectOfType<Player.Player>().Exp + " опыта";
        _winPanel.SetActive(true);
    }

    public void Lose()
    {
        GameLog.Instance.Log("[Вы проиграли!]");
        _savedAnswersId.Clear();
        RestartGame();
        // _goldText.text = FindObjectOfType<Inventory>().Items[0] + " золота";
        // _expText.text = FindObjectOfType<Player.Player>().Exp + " опыта";
        // _gameOverPanel.SetActive(true);
    }
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}