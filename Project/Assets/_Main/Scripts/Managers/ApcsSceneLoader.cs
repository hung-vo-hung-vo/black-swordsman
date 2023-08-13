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
        var next = StoryGameManager.Instance.GetSimpleNextLevel();
        Debug.Log(next);
        SceneManager.LoadScene(next);
    }
}