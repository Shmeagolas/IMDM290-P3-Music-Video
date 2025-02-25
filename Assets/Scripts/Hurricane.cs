using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{
    [SerializeField] private int numSpheres = 100;
    [SerializeField] private float outerRadius = 5f, innerRadius = 2f, minSpeed = 4f, maxSpeed = 10f, height = 0f;
    [SerializeField] private float sizeScalar = 1f, speedScalar = 1f;
    [SerializeField] GameObject spherePrefab;

    private GameObject[] spheres;
    private float[] angles, speeds, ellipseAngles;
    private Vector2[] baseEllipticalAxes;

    void Start()
    {
        Application.targetFrameRate = 60;
        spheres = new GameObject[numSpheres];
        angles = new float[numSpheres];
        speeds = new float[numSpheres];
        ellipseAngles = new float[numSpheres];
        baseEllipticalAxes = new Vector2[numSpheres];

        for (int i = 0; i < numSpheres; i++)
        {
            spheres[i] = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);
            
            angles[i] = Random.Range(0f, 2f * Mathf.PI);
            ellipseAngles[i] = Random.Range(0f, 2f * Mathf.PI);

            float a = ScewedRand(innerRadius, outerRadius);
            float b = ScewedRand(innerRadius, outerRadius);

            baseEllipticalAxes[i] = new Vector2(a, b);

            float speedScalarFactor = (a + b) / (outerRadius + outerRadius);
            speeds[i] = BellCurveRand(minSpeed, maxSpeed, 2) * speedScalarFactor;
        }
    }

    void Update()
    {
        float h = height;
        float currentAmp = AudioSpectrum.audioAmp;
        currentAmp = ((int) (currentAmp * 10f)) / 10f;
        sizeScalar = Mathf.Lerp(.5f, 2f, currentAmp / 3f);
        speedScalar = Mathf.Lerp(0.5f, 2f, currentAmp / 3f);

        for (int i = 0; i < numSpheres; i++)
        {
            angles[i] += speeds[i] * speedScalar * Time.deltaTime;
            Vector2 scaledAxes = baseEllipticalAxes[i] * sizeScalar;
            spheres[i].transform.position = new Vector3(findXEllipse(i, angles[i], scaledAxes), h, findZEllipse(i, angles[i], scaledAxes));
        }
    }

    private float BellCurveRand(float min, float max, int times)
    {
        float sum = 0;
        for(int i = 0; i < times; i++)
        {
            sum += Random.Range(min, max);
        }
        return sum / times; 
    }

    private float ScewedRand(float a, float b)
    {
        return (a - b) * Mathf.Sqrt(Random.Range(0f, 1f)) + b;
    }

    private float findXEllipse(int i, float t, Vector2 axes)
    {
        float a = ellipseAngles[i];
        return axes.x * Mathf.Cos(t) * Mathf.Cos(a) + axes.y * Mathf.Sin(t) * Mathf.Sin(a);
    }

    private float findZEllipse(int i, float t, Vector2 axes)
    {
        float a = ellipseAngles[i];
        return -1 * axes.x * Mathf.Cos(t) * Mathf.Sin(a) + axes.y * Mathf.Sin(t) * Mathf.Cos(a);
    }
}
