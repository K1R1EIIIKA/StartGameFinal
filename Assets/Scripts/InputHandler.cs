using UI;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private AnswersPanel _answersPanel;

    private void Update()
    {
        for (var i = 1; i <= 9; i++)
            if (Input.GetKeyDown(i.ToString()))
            {
                _answersPanel.OnNumberPressed(i);
            }
    }
}