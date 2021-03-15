using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MenuManager.Menu
{
    MenuManager.Menu levelMenu;
    MenuManager manager;
    MenuButton.Data[] layout;
    GameController gc;
    AudioSource actionSound;

    public MainMenu(MenuManager _manager, MenuManager.Menu _levelMenu, AudioSource _actionSound)
    {
        manager = _manager;
        levelMenu = _levelMenu;
        actionSound = _actionSound;
        gc = GameController.Instance;
        layout = new MenuButton.Data[4];
        layout[0] = new MenuButton.Data("Play", true, 1, 0, levelMenu);
        layout[1] = new MenuButton.Data("Sound: ", true, 2, 0, null);
        layout[2] = new MenuButton.Data("Quit", true, 1, 0, null);
        layout[3] = new MenuButton.Data("Credits", true, 2, 200, null);

        layout[1].text = "Sound: " + (gc.sound ? "On" : "Off");
        actionSound.volume = gc.sound ? 1 : 0;
    }


    override public void Display()
    {
        manager.SetButtons(layout);
    }

    override public void ExecOperation(MenuButton.Data data)
    {
        if (data.auxValue == 200)
        {
            SceneManager.LoadScene(21);
            return;
        }

        gc.sound = !gc.sound;
        layout[1].text = "Sound: " + (gc.sound ? "On" : "Off");
        manager.SetButtons(layout);
        actionSound.volume = gc.sound ? 1 : 0;
    }
}
