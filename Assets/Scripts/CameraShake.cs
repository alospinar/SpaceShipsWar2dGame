using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude= 0.5f;

    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }
    public void play()
    {
        StartCoroutine(ShakeCamera());
    }
    IEnumerator ShakeCamera()
    {
        float timerElapsed = 0f;
        while (timerElapsed<shakeDuration)
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle*shakeMagnitude;
            timerElapsed += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
