using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool Is_Goal=false;
    private bool LoadRequested = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (LoadRequested)
        {
            LoadRequested = false;

            int nextSceneIndex =
                SceneManager.GetActiveScene().buildIndex + 1;

            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }
    }

    public void StartGame()
    {
        LoadRequested = false;
    }

    public void ClearGame()
    {
        if (Is_Goal) return;

        Debug.Log("ClearGame");

        Is_Goal = true;

        LoadRequested = true;
    }

    public void ResetGameDate()
    {
        Is_Goal = false;
        LoadRequested = false;
    }
}
