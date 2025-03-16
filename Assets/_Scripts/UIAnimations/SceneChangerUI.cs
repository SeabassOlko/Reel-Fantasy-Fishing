using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerUI : MonoBehaviour
{
    [SerializeField]
    Animator leftAnim, rightAnim, middleAnim;

    string sceneName = "";

    private void Start()
    {
        OpenScreen();
    }

    public void SetSceneToLoad(string name)
    {
        sceneName = name;
    }

    public void OpenScreen()
    {
        leftAnim.SetTrigger("Open");
        rightAnim.SetTrigger("Open");
        middleAnim.SetTrigger("Open");
    }

    public void CloseScreen()
    {
        leftAnim.SetTrigger("Close");
        rightAnim.SetTrigger("Close");
        middleAnim.SetTrigger("Close");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
