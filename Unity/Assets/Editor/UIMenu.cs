using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UrbanTimeTravel.UI { 
    public class UIMenu : MonoBehaviour
    {
        [MenuItem("TimeTravel/UI/New UI Screen")]
        private static void AddUIScreen()
        {
            CreateUIScreen("UIScreen");
        }

        [MenuItem("TimeTravel/UI/New Timed UI Screen")]
        private static void AddTimedUIScreen()
        {
            CreateUIScreen("UITimedScreen");
        }

        [MenuItem("TimeTravel/UI/Presets/New Video Player")]
        private static void AddVideoScreen()
        {
            CreateUIScreen("VideoScreen");
        }

        [MenuItem("TimeTravel/UI/Presets/New Image Viewer")]
        private static void AddImageView()
        {
            CreateUIScreen("ImageScreen");
        }

        [MenuItem("TimeTravel/UI/Presets/New Text ScrollView")]
        private static void AddTextScrollview()
        {
            CreateUIScreen("ScrollViewScreen");
        }

        [MenuItem("TimeTravel/UI/Presets/New Infopanel")]
        private static void AddInfoPanelview()
        {
            CreateUIScreen("InfoPanelScreen");
        }

        private static void CreateUIScreen(string prefabName)
        {
            GameObject screen = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/"+prefabName+".prefab");
            if(screen != null)
            {
                GameObject newScreen;

                if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<UrbanTimeTravel.UI.UIGroup>() != null)
                {
                    newScreen = Instantiate(screen, Selection.activeTransform);
                    newScreen.name = "New UI Screen";
                }
                else
                {
                    if(prefabName != "UIScreen")
                    {
                        EditorUtility.DisplayDialog("Warning", "Select UI Group or create a new one (UI->New UI Screen).", "Ok");
                        return;
                    }

                    screen = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/UI/UIGroup.prefab");
                    newScreen = Instantiate(screen);
                    newScreen.name = "New UI Group";
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Warning", "UIScreen prefab not found.", "Ok");
            }
        }
    }
}