using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float shakeIntensity;
    [SerializeField] float shakeDuration;

    void Update()
    {
        if (shakeDuration > 0)
        {
            float shake = shakeDuration * shakeIntensity;
            transform.localPosition = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), 0);
            shakeDuration -= Time.deltaTime;
        }
        else transform.localPosition = Vector3.zero;
    }
    public void ScreenShake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
    }
}