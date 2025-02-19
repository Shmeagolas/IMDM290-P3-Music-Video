using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rain : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dropPrefab;
    public float height = 10f;
    public float xRange = 4f;
    public float zRange = 4f;
    private float previousAmp = 0f;


    void Start()
    {
        Camera.main.transform.position = new Vector3(0f, 5.87f, -6.91f);
        Camera.main.transform.rotation = Quaternion.Euler(45.723f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float currentAmp = AudioSpectrum.audioAmp;
        Debug.Log("Current Audio Amp: " + currentAmp * 1000);

        if (currentAmp > previousAmp + 0.05f) {
            SpawnRaindrop();
        }

        previousAmp = currentAmp;
    }

    void SpawnRaindrop() {
        Vector3 position = new Vector3(Random.Range(-xRange, xRange), height, Random.Range(-zRange, zRange));
        Instantiate(dropPrefab, position, Quaternion.identity);
    }

}

