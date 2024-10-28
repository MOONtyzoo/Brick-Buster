using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelPrefabs;
    [SerializeField] private Readouts readouts;

    private int currentLevel = 0;
    private GameObject levelGameObject;

    public void GoToNextLevel() {
        LoadLevel(currentLevel+1);
    }

    public void LoadLevel(int newLevel) {
        if (newLevel < 0 || newLevel >= levelPrefabs.Count) {
            Debug.LogError("Loading level failed, this level doesn't exist!");
            return;
        }

        currentLevel = newLevel;
        readouts.ShowLevel(currentLevel);

        if (levelGameObject != null)
            Destroy(levelGameObject);
        
        levelGameObject = Instantiate(levelPrefabs[currentLevel], gameObject.transform);
    }

    public bool IsLastLevel() {
        return (currentLevel == levelPrefabs.Count-1);
    }

    public bool IsLevelCompleted() {
        int bricksRemaining = GameObject.FindGameObjectsWithTag("Brick").Where(brick => brick.activeInHierarchy).Count();
        return (bricksRemaining == 0);
    }
}
