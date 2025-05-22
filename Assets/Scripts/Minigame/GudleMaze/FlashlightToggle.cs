using UnityEngine;

public class FlashlightToggle : MonoBehaviour
{
    public Light flashlight;  // Spot Light �����
    private bool isOn = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}
