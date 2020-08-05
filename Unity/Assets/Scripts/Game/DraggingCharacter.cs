using TMPro;
using UnityEngine;

namespace Game
{
    public class DraggingCharacter : MonoBehaviour
    {
        private TextMeshProUGUI _characterText;
        public char Character { get; private set; }

        public void Initialize()
        {
            _characterText = GetComponent<TextMeshProUGUI>();
            _characterText.text = "";
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void SetCharacter(char character)
        {
            _characterText.text = character.ToString();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}