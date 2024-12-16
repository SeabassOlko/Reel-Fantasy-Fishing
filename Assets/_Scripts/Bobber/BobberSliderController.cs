using UnityEngine;
using UnityEngine.UI;

public class BobberSliderController : MonoBehaviour
{
    [SerializeField]
    Slider TensionSlider;
    [SerializeField]
    Slider ProgressSlider;

    float tension, progress;
    float MaxTension = 10f, MaxProgress = 10f;
    
    public void AddTension(float amount)
    {
        tension = Mathf.Clamp(tension + amount, 0f, MaxTension);
        UpdateTensionSlider();
    }
    public void RemoveTension(float amount) 
    {
        tension = Mathf.Clamp(tension - amount, 0f, MaxTension);
        UpdateTensionSlider();
    }

    public float GetTension()
    {
        return tension / MaxTension;
    }

    public float GetProgress()
    {
        return progress / MaxProgress;
    }

    public void AddProgress(float amount)
    {
        progress = Mathf.Clamp(progress + amount, 0f, MaxProgress);
        UpdateProgressSlider();
    }

    public void RemoveProgress(float amount)
    {
        progress = Mathf.Clamp(progress - amount, 0f, MaxProgress);
        UpdateProgressSlider();
    }

    public void SetUpSliders()
    {
        progress = MaxProgress / 4;
        tension = 0;
        UpdateTensionSlider();
        UpdateProgressSlider();
    }

    void UpdateTensionSlider()
    {
        TensionSlider.value = tension / MaxTension;
    }

    void UpdateProgressSlider()
    {
        ProgressSlider.value = progress / MaxProgress;
    }
}
