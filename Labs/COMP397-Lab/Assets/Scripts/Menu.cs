using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : PersistentSingleton<Menu>
{
    [SerializeField] private Button saveBtn;
    [SerializeField] private Button loadBtn;

    private void Start()
    {
        saveBtn.onClick.AddListener(() =>
        {
            SaveLoadSystem.instance.gameData.fileName = "Menu";
            SaveLoadSystem.instance.gameData.sceneName = "SampleScene";
            SaveLoadSystem.instance.SaveGame();
        });

        loadBtn.onClick.AddListener(() =>
        {
            SaveLoadSystem.instance.LoadGame("Menu");
        });
    }
}
