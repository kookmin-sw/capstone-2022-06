using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class Icons
{
    public List<HeroInfo> Contents;
}

[System.Serializable]
class HeroInfo 
{
    public string path;
}

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

        // Delete dummy icons
        foreach (Transform child in contentsDiv.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        FileInfo jsonInfo = new FileInfo("Assets/Scripts/JSON/HeroIcons.json");

        if (jsonInfo.Exists is not false)
        {
            string jsonString = File.ReadAllText(jsonInfo.FullName);
            Icons loadedIcons = JsonUtility.FromJson<Icons>(jsonString);

            for (int i = 0; i < loadedIcons.Contents.Count; i++)
            {
                HeroInfo info = loadedIcons.Contents[i];
                Sprite heroPortrait = Managers.Resource.Load<Sprite>(info.path);
                UI_Portrait portrait = Managers.UI.AttachSubItem<UI_Portrait>(contentsDiv.transform);
                portrait.GetComponent<Image>().sprite = heroPortrait;
                portrait.name = $"Portrait_{i + 1}";

                // int idx = portrait.name.IndexOf("(Clone)");
                // portrait.name = portrait.name.Replace(portrait.name.Substring(idx, 7), (i + 1).ToString());
            }
        }
        else
        {
            Debug.Log($"Failed to load json file {jsonInfo.Name}");
        }

        // Managers.UI.AttachSubItem
    }
}
