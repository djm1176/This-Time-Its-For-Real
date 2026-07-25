using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Shaking")]
    public float shakeSpeed;
    public float shakeDecay;
    float shakeAmount;
    // Update is called once per frame
    void Update()
    {
        // Camera shake
        float noiseX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0) - 0.5f;
        float noiseY = Mathf.PerlinNoise(0, Time.time * shakeSpeed) - 0.5f;
        transform.position += new Vector3(noiseX * shakeAmount, noiseY * shakeAmount, 0);
        shakeAmount = Mathf.Lerp(shakeAmount, 0f, shakeDecay * Time.deltaTime);
    }

    public void Shake(float amount)
    {
        shakeAmount += amount;
    }
}
