using System;
using System.Linq;
using GameConfiguration;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AnswerButton : MonoBehaviour
    {
        [SerializeField] private Button _mainButton;
        [SerializeField] private TMP_Text _text;

        private void OnValidate()
        {
            _mainButton = GetComponent<Button>();
        }

        public void Init(int number, DialogVariant variant)
        {
            _text.text = $"[{number}] {variant.text}";
            foreach (var action in variant.actions)
            {
                object[] objArray = action.parameters.Cast<object>().ToArray();

                _mainButton.onClick.AddListener(() =>
                    MethodFromStringExecuter.Instance.InvokeMethod(action.name, objArray));
                // _mainButton.onClick.AddListener(() => GameLog.Instance.Log(action.name, objArray));

            }

            _mainButton.onClick.AddListener(() => Game.Instance.GoToDialog(variant.to));
            _mainButton.onClick.AddListener(() => GameLog.Instance.Log(_text.text));
            _mainButton.onClick.AddListener(() => Game.Instance.SaveAnswer(variant.id));
            // Debug.Log(String.Join(", ", Game.Instance.GetSavedAnswers()));
        }

        private void OnDestroy()
        {
            _mainButton.onClick.RemoveAllListeners();
        }

        public void Click() =>
            _mainButton.onClick.Invoke();
    }
}