using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public MeshRenderer buttonMesh;
    public TextMesh text;

    bool highlighted;

    public class Data
    {
        public Data(string _text, bool _enabled, int _opcode, int _auxValue, MenuManager.Menu _auxMenu)
        {
            text = _text;
            enabled = _enabled;
            opCode = _opcode;
            auxValue = _auxValue;
            auxMenu = _auxMenu;
        }

        // Text to display on the button
        public string text;
        public bool enabled;
        // 1 - Change menu, 2 - exec action
        public int opCode;
        // Value storing the levels
        public int auxValue;
        // Auxiliary menu for menu changing ops
        public MenuManager.Menu auxMenu;
    }

    public Data data;

    MenuManager mManager;

    public void Awake()
    {
        mManager = GetComponentInParent<MenuManager>();
    }

    public void setText(string t)
    {
        text.text = t;
    }

    public void setData(Data _data)
    {
        data = _data;
        setText(data.text);
        refreshMaterial();
    }

    public void setHighlighted(bool _highlighted)
    {
        highlighted = _highlighted;
        refreshMaterial();
    }

    public void refreshMaterial()
    {
        if (data.enabled)
            buttonMesh.material = highlighted ? mManager.matEnabledOn : mManager.matEnabledOff;
        else
            buttonMesh.material = highlighted ? mManager.matDisabledOn : mManager.matDisabledOff;
    }
}
