using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{
    [SerializeField] private int numSpheres = 100;
    [SerializeField] private float outerRadius = 5f, innerRadius = 2f, minSpeed = 4f, maxSpeed = 10f;
    [SerializeField] GameObject spherePrefab;

    private GameObject[] spheres;
    private float[] angles, speeds, ellipseAngles;
    private Vector2[] ellipticalAxes;

    void Start()
    {
        spheres = new GameObject[numSpheres];
        angles = new float[numSpheres];
        speeds = new float[numSpheres];
        ellipseAngles = new float[numSpheres];
        ellipticalAxes = new Vector2[numSpheres];
       


        for (int i = 0; i < numSpheres; i++)
        {

            spheres[i] = Instantiate(spherePrefab, Vector3.zero, Quaternion.identity);
            
            angles[i] = Random.Range(0f, 2f * Mathf.PI);
            ellipseAngles[i] = Random.Range(0f, 2f * Mathf.PI);


            float a = ScewedRand(innerRadius, outerRadius); 
            float b = ScewedRand(innerRadius, outerRadius);

            ellipticalAxes[i] = new Vector2(a, b);

            float speedScalar = (a + b) / (outerRadius + outerRadius);
            speeds[i] = BellCurveRand(minSpeed, maxSpeed, 2) * speedScalar;
        }
    }

    void Update()
    {
        for (int i = 0; i < numSpheres; i++)
        {

            angles[i] += speeds[i] * Time.deltaTime;
            spheres[i].transform.position = new Vector3(findXEllipse(i, angles[i]), 0, findZEllipse(i, angles[i]));

            float nextAngle = angles[i] + speeds[i] * Time.deltaTime;
            Vector3 nextPosition = new Vector3(findXEllipse(i, nextAngle), 0, findZEllipse(i, nextAngle));

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

    private float findXEllipse(int i, float t)
    {
        //x = pcos(θ)cos(a)+qsin(θ)sin(a)
        float a = ellipseAngles[i];
        return ellipticalAxes[i].x * Mathf.Cos(t) * Mathf.Cos(a) + ellipticalAxes[i].y * Mathf.Sin(t) * Mathf.Sin(a);
    }

    private float findZEllipse(int i, float t)
    {
        //z = −pcos(θ)sin(a)+qsin(θ)cos(a)
        float a = ellipseAngles[i];
        return -1 * ellipticalAxes[i].x * Mathf.Cos(t) * Mathf.Sin(a) + ellipticalAxes[i].y * Mathf.Sin(t) * Mathf.Cos(a);
    }
}
