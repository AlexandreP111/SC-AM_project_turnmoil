using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MenuManager.Menu
{
    GameController gc;
    MenuManager mm;
    MenuManager.Menu mainMenu;

    int page;

    public LevelMenu(MenuManager _mm)
    {
        gc = GameController.Instance;
        mm = _mm;
        mainMenu = null;
    }

    public void setMainMenu(MenuManager.Menu _mainMenu)
    {
        mainMenu = _mainMenu;
    }

    override public void Display()
    {
        page = 0;
        UpdateLayout();
    }

    void UpdateLayout()
    {
        MenuButton.Data[] layout = new MenuButton.Data[6];
        int lCounter = 0;
        int levelCounter = page * 4;
        for (; levelCounter < (page + 1) * 4; ++levelCounter)
        {
            if (levelCounter < (gc.lastLevelCompleted + 1))
                layout[lCounter++] = new MenuButton.Data("Level " + (levelCounter + 1), true, 2, levelCounter + 1, null);
            else
                layout[lCounter++] = new MenuButton.Data("Level " + (levelCounter + 1), false, 0, levelCounter + 1, null);
        }

        layout[4] = new MenuButton.Data("More", true, 2, 500, null);
        layout[5] = new MenuButton.Data("Back", true, 1, 0, mainMenu);

        mm.SetButtons(layout);
    }

    override public void ExecOperation(MenuButton.Data data)
    {
        if (data.auxValue == 500)
        {
            // Next page
            page++;
            if (page > 4) page = 0;
            UpdateLayout();
        }
        else
        {
            // Change scene
            SceneManager.LoadScene(data.auxValue);
        }
    }
}
