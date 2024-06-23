using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChanger : MonoBehaviour
{
    public ObjectHider CurrentMenu;

    public void ChangeMenu(ObjectHider menu)
    {
        if (CurrentMenu != null)
        {
            if (menu != null)
            {
                if(CurrentMenu == menu)
                {
                    menu.ChangeVisibility();
                }
            }
        }

        if(menu != null)
        {
            if (CurrentMenu != menu)
            {
                menu.Show();
                CurrentMenu = menu;
            }
        }
    }
}
