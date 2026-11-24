using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GoalScene : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Stage1";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.ResetGameDate();
            }

            //ゲームシーンへ遷移
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
