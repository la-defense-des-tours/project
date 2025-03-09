using Core.Camera;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private DayNightController dayNightController;
    [SerializeField] private Light[] lights;

    void Start()
    {
        foreach (Light light in lights)
            light.color = Color.blue;
    }
    void Update()
    {
        UpdateLight();
    }
    
    private void UpdateLight()
    {
        if (!dayNightController)
            return;

        foreach (Light light in lights)
            light.intensity = dayNightController.IsDay() ? 0 : 500;
    }
}
