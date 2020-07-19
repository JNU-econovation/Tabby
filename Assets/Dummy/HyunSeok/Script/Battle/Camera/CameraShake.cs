using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake _instance;
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    //public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
   // public float shakeAmount = 0.7f;
    //public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);

        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
        originalPos = camTransform.localPosition;
    }

    public IEnumerator ShakeCamera(float shakeDuration, float shakeAmount, float decreaseFactor)
    {
        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime / shakeDuration;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * (shakeAmount * (1 - progress));
            yield return null;
        }
        shakeDuration = 0f;
        camTransform.localPosition = originalPos;
    }

}