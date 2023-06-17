using UnityEngine;
using UnityEngine.SceneManagement;

public class ApcsSceneLoader : Singleton<ApcsSceneLoader>
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadStoryGame()
    {
        SceneManager.LoadScene(StoryGameManager.Instance.GetNextLevel());
    }
}