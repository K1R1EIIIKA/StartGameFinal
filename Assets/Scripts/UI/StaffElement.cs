using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StaffElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Image _image;

        public void Init(Sprite image, Action action = null)
        {
            if (image)
                _image.sprite = image;

            if (action != null)
                _image.AddComponent<Button>().onClick.AddListener(() => action());
        }

        public void ChangeCollect(int count) => _countText.text = count.ToString();
    }
}