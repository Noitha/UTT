using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class CrosswordCharacter : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private Crossword _crossword;

        private TextMeshProUGUI _text;
        private char _character;
        
        public void Initialize(Crossword crossword, char character)
        {
            _crossword = crossword;
            _character = character;
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _text.text = _character.ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _crossword.InitializeDrag(_character);
            UpdateCharacterPosition();
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateCharacterPosition();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var pointerEnter = eventData.pointerEnter;
            
            if (pointerEnter == null)
            {
                return;
            }

            var crosswordSlot = pointerEnter.GetComponent<CrosswordSlot>();
            
            if (crosswordSlot)
            {
                crosswordSlot.SetCharacter(_character);
            }
           
            _crossword.ResetDraggingCharacter();
        }

        private void UpdateCharacterPosition()
        {
            _crossword.UpdateDraggingCharacterPosition(Input.mousePosition);
        }
    }
}