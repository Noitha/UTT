using TMPro;
using UnityEngine;

namespace Game
{
    public class CrosswordSlot : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        public bool IsCorrect { get; private set; }
        
        public void Initialize()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetCharacter(char character)
        {
            _text.text = character.ToString();
        }
    }
}