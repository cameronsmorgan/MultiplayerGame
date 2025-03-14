using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 originalPosition;
    private float shakeDuration = 0.5f;
    private float shakeMagnitude = 0.05f;
    private float dampingSpeed = 1.0f;

    void Awake()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogError("No main camera found!");
        }
    }

    void OnEnable()
    {
        originalPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}