using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartPlayGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    }

    public void StartPlayGameSingle()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
