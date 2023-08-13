using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryGameManager : Singleton<StoryGameManager>
{
    const string _COMPLETED_LEVELS_KEY = "completedLevels";
    const string _CURRENT_LEVEL_KEY = "currentLevel";

    [SerializeField] List<string> _levels = new List<string>();
    [field: SerializeField] public GameObject PlayerPrefab { get; private set; }

    List<string> _completedLevels;
    string _currentLevel = null;

    protected override void Awake()
    {
        base.Awake();
        // LoadGame();
    }

    public void SaveGame()
    {
        ES3.Save<List<string>>(_COMPLETED_LEVELS_KEY, _completedLevels);
        ES3.Save<string>(_CURRENT_LEVEL_KEY, _currentLevel);
    }

    public string GetSimpleNextLevel()
    {
        if (_currentLevel == null)
        {
            _currentLevel = _levels[0];
            return _currentLevel;
        }

        _currentLevel = _currentLevel == _levels[0] ? _levels[1] : _levels[0];
        return _currentLevel;
    }

    public string GetNextLevel()
    {
        var levels = _levels.Where(lv => !_completedLevels.Contains(lv)).ToList();
        if (levels.Count == 0)
        {
            return null;
        }

        _currentLevel = levels[Random.Range(0, levels.Count)];
        return _currentLevel;
    }

    public void CompleteCurrentLevel()
    {
        _completedLevels.Add(_currentLevel);
        _currentLevel = null;
    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
        base.OnApplicationQuit();
    }

    private void LoadGame()
    {
        try
        {
            _completedLevels = ES3.Load<List<string>>(_COMPLETED_LEVELS_KEY);
            _currentLevel = ES3.Load<string>(_CURRENT_LEVEL_KEY);
        }
        catch (System.Exception exp)
        {
            Debug.LogError(exp);

            _completedLevels = new List<string>();
            _currentLevel = null;
        }
    }
}