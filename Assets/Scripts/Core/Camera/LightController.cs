using Core.Camera;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private DayNightController dayNightController;
    [SerializeField] private Light[] lights;

    void Update()
    {
        UpdateLight();
    }
    
    private void UpdateLight()
    {
        if (!dayNightController)
            return;

        foreach (Light light in lights)
        {
            if (dayNightController.IsDay())
                light.intensity = 0;
            else
                light.intensity = 250;
        }
    }
}
