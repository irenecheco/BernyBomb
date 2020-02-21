using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManagerMenu : MonoBehaviour
{
    public RectTransform mainMenu, settingsMenu;

    public void SettingsBtn()
    {
        mainMenu.DOAnchorPos(new Vector2(0, -1080), 0.45f);
        settingsMenu.DOAnchorPos(new Vector2(0, 0), 0.45f);
    }
}
