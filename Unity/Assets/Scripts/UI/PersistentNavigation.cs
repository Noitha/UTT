using UnityEngine;
using UrbanTimeTravel.UI;
using UnityEngine.UI;

public class PersistentNavigation : MonoBehaviour
{
    #region Variables

    public UIScreen[] tabMenu;
    public Button[] tabButton;
    private int selectedTab=0;

    #endregion

    #region Main Methods

    void Start()
    {
        for (int index = 0; index < tabButton.Length; index++)
        {
            //needed for ButtonAction
            //using index will break all buttons due to it changing over the lifespan of the loop
            int i = index+1;
            tabButton[index].onClick.AddListener(()=>ButtonAction(i));
        }
    }

    #endregion

    #region Helper Methods

    public void ButtonAction(int wantedTabNumber)
    {
        if (selectedTab == wantedTabNumber)
        {
            tabMenu[wantedTabNumber].CloseScreen();
            tabButton[wantedTabNumber-1].GetComponent<Image>().color = new Color(0.2f,0.2f,0.2f,0f);
            tabMenu[0].OpenScreen();
            selectedTab = 0;
        }
        else
        {
            tabMenu[selectedTab].CloseScreen();
            
            if(selectedTab > 0 && selectedTab <= tabButton.Length)
            {
                tabButton[selectedTab-1].GetComponent<Image>().color = new Color(0.2f,0.2f,0.2f,0f);
            }
            if (wantedTabNumber <= tabButton.Length)
            {
                tabButton[wantedTabNumber-1].GetComponent<Image>().color = new Color(0.2f,0.2f,0.2f,0.2f);
            }
                
            tabMenu[wantedTabNumber].OpenScreen();
            selectedTab = wantedTabNumber;
        }
    }

    #endregion
}
