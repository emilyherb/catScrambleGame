using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        StartCoroutine(DoShake());
    }

    private IEnumerator DoShake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}