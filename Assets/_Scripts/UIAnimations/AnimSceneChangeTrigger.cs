using UnityEngine;

public class AnimSceneChangeTrigger : MonoBehaviour
{
    void AnimFinished()
    {
        FindAnyObjectByType<SceneChangerUI>().LoadScene();
    }
}
