using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuManager : MonoBehaviour
{
    public MenuButton[] buttons;
    public Material matEnabledOn;
    public Material matEnabledOff;
    public Material matDisabledOn;
    public Material matDisabledOff;
    public AudioSource actionSound;

    int currentButton;
    Menu currentMenu;

    MainMenu menu_mainMenu;
    LevelMenu menu_levelMenu;

    public class Menu
    {
        public virtual void Display() { }
        public virtual void ExecOperation(MenuButton.Data op) { }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentButton = 0;

        menu_levelMenu = new LevelMenu(this);
        menu_mainMenu = new MainMenu(this, menu_levelMenu, actionSound);
        menu_levelMenu.setMainMenu(menu_mainMenu);

        currentMenu = menu_mainMenu;
        menu_mainMenu.Display();
    }

    // Bt data must be 6 slots
    public void SetButtons(MenuButton.Data[] btData)
    {
        int btCount = btData.Length;
        int counter = 0;
        for (; counter < btCount; ++counter)
        {
            buttons[counter].gameObject.SetActive(true);
            buttons[counter].setData(btData[counter]);
        }

        for (; counter < 6; ++counter)
        {
            buttons[counter].gameObject.SetActive(false);
        }

        recalculateSelected();
    }

    void pressButton()
    {
        MenuButton.Data op = buttons[currentButton].data;
        if (!op.enabled)
            return;
        switch (op.opCode)
        {
            case 1:
                if (op.auxMenu == null)
                {
                    Application.Quit();
                }
                else
                {
                    currentMenu = op.auxMenu;
                    op.auxMenu.Display();
                }
                break;
            case 2:
                currentMenu.ExecOperation(op);
                break;
        }
    }

    void recalculateSelected()
    {
        if (buttons[currentButton].gameObject.activeSelf)
        {
            selectButton(currentButton);
        }
        selectButton(0);
    }

    void selectButton(int button)
    {
        if (button < 0 || button >= 6 || !buttons[button].gameObject.activeSelf)
            return;
        foreach (MenuButton mb in buttons)
        {
            if (mb.gameObject.activeSelf)
                mb.setHighlighted(false);
        }
        buttons[button].setHighlighted(true);
        currentButton = button;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            actionSound.Play();
            selectButton(currentButton + 1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            actionSound.Play();
            selectButton(currentButton - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            actionSound.Play();
            pressButton();
        }
    }
}
