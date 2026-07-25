using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;
    public Vector3 targetOffset;
    public float speed;

    [Header("Shaking")]
    public float shakeSpeed;
    public float shakeDecay;
    float shakeAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vTarget = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, vTarget, speed * Time.deltaTime);

        Quaternion qTarget = Quaternion.LookRotation(target.transform.position - transform.position + targetOffset, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, qTarget, speed * Time.deltaTime);


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
