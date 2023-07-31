using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject _container;

    public void BackToHome()
    {
        ApcsSceneLoader.Instance.LoadStartMenu();
    }

    private void Start()
    {
        _container.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _container.SetActive(!_container.activeSelf);
        }
    }

    public void Open()
    {
        _container.SetActive(true);
    }
}