using System.Collections.Generic;
using GameConfiguration;
using UnityEngine;

namespace UI
{
    public class AnswersPanel : MonoBehaviour
    {
        [SerializeField] private AnswerButton _answerButtonPrefab;

        private List<AnswerButton> _currentAnswersButton = new List<AnswerButton>();
        private List<DialogVariant> _currentVariants;
    
        public void Init(List<DialogVariant> variants)
        {
            _currentVariants = variants;
            
            foreach (var button in _currentAnswersButton)
            {
                Destroy(button.gameObject);
            }
            
            _currentAnswersButton.Clear();
        
            foreach (var variant in variants)
            {
                var newButton = Instantiate(_answerButtonPrefab, transform);
                _currentAnswersButton.Add(newButton);
                newButton.Init(_currentAnswersButton.IndexOf(newButton) + 1, variant);
            }
        }

        public void OnNumberPressed(int number)
        {
            if(number > _currentAnswersButton.Count || !gameObject.active) return;
            
            _currentAnswersButton[number - 1].Click();
        }
    }
}
