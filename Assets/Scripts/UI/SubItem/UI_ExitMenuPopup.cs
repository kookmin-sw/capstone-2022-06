using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExitMenuPopup : UI_Base
{
    enum Buttons
    {
        ConfirmButton,
        CancelButton
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.CancelButton).onClick.AddListener(() => {
            gameObject.SetActive(false);
        });

        GetButton((int)Buttons.ConfirmButton).onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
