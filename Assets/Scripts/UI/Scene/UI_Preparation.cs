using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Preparation : UI_Scene
{
    private GameObject contentsDiv = null;

    enum GameObjects
    {
        SelectedView,
        PickUpBoard,
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        contentsDiv = Util.SearchChild(
            Util.SearchChild(Get<GameObject>((int)GameObjects.PickUpBoard), "Viewport"),
            "ContentsDiv"
        );

        // May delete place holder ui objects but not delete yet.
        // foreach (Transform child in contentsDiv.transform)
        // {
        //     Managers.Resource.Destroy(child.gameObject);
        // }


        // Managers.UI.AttachSubItem
    }
}
