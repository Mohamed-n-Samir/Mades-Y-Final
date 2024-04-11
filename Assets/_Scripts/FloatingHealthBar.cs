using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        initialRotation = Quaternion.Inverse(transform.parent.rotation) * transform.rotation;
    }

    void LateUpdate()
{
    // Get the current rotation of the parent
    Quaternion parentRotation = transform.parent.rotation;

    // Construct the new rotation with the child's Y-axis rotation fixed
    Quaternion newRotation = parentRotation * initialRotation;

    // Apply the new rotation to the child
    transform.localRotation = newRotation;
}

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        healthSlider.value = currentValue / maxValue;
    }
}
