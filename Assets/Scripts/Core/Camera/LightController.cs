using Core.Camera;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private DayNightController dayNightController;
    [SerializeField] private Light[] lights;

    void Start()
    {
        SetupLights();
    }

    void Update()
    {
        UpdateLight();
    }

    private void SetupLights()
    {
        foreach (Light light in lights)
        {
            light.color = Color.white;
        }
    }
    
    private void UpdateLight()
    {
        if (!dayNightController)
            return;

        foreach (Light light in lights)
        {
            if (dayNightController.IsDay())
                light.intensity = 250;
            else
                light.intensity = 0;
        }
    }
    
    
}
