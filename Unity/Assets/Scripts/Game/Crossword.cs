using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Crossword : MonoBehaviour
    {
        [Header("Prefabs")]
        public CrosswordCharacter crosswordCharacterPrefab;
        public CrosswordSlot crosswordSlotPrefab;
        
        [Header("Container")]
        public Transform characterContainer;
        public GridLayoutGroup slotContainerGridLayoutGroup;
        
        public DraggingCharacter draggingCharacter;
        public Button switchKeyboardLayout;
        private string _currentKeyboardLayout;

        private static readonly Dictionary<string, List<List<char>>> _keyboardLayouts = new Dictionary<string, List<List<char>>>
        {
            {
                "Qwertz",
                new List<List<char>>
                {
                    new List<char>{'Q', 'W', 'E', 'R', 'T', 'Z', 'U', 'I', 'O', 'P'},
                    new List<char>{'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L'},
                    new List<char>{'Y', 'X', 'C', 'V', 'B', 'N', 'M',}
                }
            },
            {
                "Azerty",
                new List<List<char>>
                {
                    new List<char>{'A', 'Z', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P'},
                    new List<char>{'Q', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M'},
                    new List<char>{'W', 'X', 'C', 'V', 'B', 'N'}
                }
            }
        };
        
        public void Start()
        {
            //Set the current keyboard layout to qwertz.
            _currentKeyboardLayout = "Qwertz";
            
            //Add event to the button to switch keyboard layout.
            switchKeyboardLayout.onClick.AddListener(SwitchKeyboardLayout);
            
            //GetParameterValues the dragging character component.
            draggingCharacter.Initialize();
            
            //Generate the current keyboard layout.
            GenerateKeyboardLayout(_currentKeyboardLayout);
            
            //Generate the grid of slots for the crossword.
            GenerateSlotGrid();
        }


        private void SwitchKeyboardLayout()
        {
            _currentKeyboardLayout = _currentKeyboardLayout.Equals("Qwertz") ? "Azerty" : "Qwertz";
            GenerateKeyboardLayout(_currentKeyboardLayout);
        }
        
        private void GenerateKeyboardLayout(string keyboardLayout)
        {
            if(!_keyboardLayouts.TryGetValue(keyboardLayout, out var keySet))
            {
                return;
            }

            foreach (Transform character in characterContainer)
            {
                Destroy(character.gameObject);
            }

            foreach (var line in keySet)
            {
                var characterLine = new GameObject("Character Line")
                {
                    transform =
                    {
                        parent = characterContainer,
                        localScale = Vector3.one
                    }
                };
                
                var horizontalLayoutGroup = characterLine.AddComponent<HorizontalLayoutGroup>();
                horizontalLayoutGroup.childForceExpandWidth = true;
                horizontalLayoutGroup.childForceExpandHeight = true;
                
                foreach (var character in line)
                {
                    var newCharacter = Instantiate(crosswordCharacterPrefab, characterLine.transform);
                    newCharacter.Initialize(this, character);
                }
            }
        }


        private void GenerateSlotGrid()
        {
            var size = GetComponent<RectTransform>().rect.size;
            Debug.Log(size);
            var slotSize = size.x / 10f;
            var gap = slotSize / 20f;
            var slotSizeWithGap = slotSize - 9 * gap;

            slotContainerGridLayoutGroup.cellSize = new Vector2(slotSizeWithGap, slotSizeWithGap);
            slotContainerGridLayoutGroup.spacing = new Vector2(gap, gap);
            
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    var crosswordSlot = Instantiate(crosswordSlotPrefab, slotContainerGridLayoutGroup.transform);
                    crosswordSlot.Initialize();
                }
            }
        }
        
        public void InitializeDrag(char character)
        {
            draggingCharacter.SetCharacter(character);
            draggingCharacter.Show();
        }
        
        public void ResetDraggingCharacter()
        {
            draggingCharacter.Hide();
        }
        
        public void UpdateDraggingCharacterPosition(Vector3 position)
        {
            draggingCharacter.SetPosition(position);
        }
    }
}