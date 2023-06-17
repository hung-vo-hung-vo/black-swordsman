using UnityEngine;

public class StartMenuScener : MonoBehaviour
{
    public void StartStoryGame()
    {
        ApcsSceneLoader.Instance.LoadStoryGame();
    }

    public void StartCoopGame()
    {

    }
}