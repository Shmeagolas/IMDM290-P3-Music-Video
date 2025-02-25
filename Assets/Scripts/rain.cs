using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class rain : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dropPrefab;
    public float height = 100f;
    public float xRange = 20f;
    public float zRange = 20f;
    private float previousAmp = 0f;
    [SerializeField] float forceAmount = 1;
    public int dropCounter = 0;
    float time;


    void Start()
    {
        int raindropsLayer = LayerMask.NameToLayer("Raindrops");
        Physics.IgnoreLayerCollision(raindropsLayer, raindropsLayer);
        // Camera.main.transform.position = new Vector3(0f, 5.87f, -6.91f);
        // Camera.main.transform.rotation = Quaternion.Euler(45.723f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float currentAmp = AudioSpectrum.audioAmp;
        if(time >= .4f)
        {
            if (currentAmp > previousAmp + 0.05f) {
                Debug.Log("Current Audio Amp: " + currentAmp * 1000);
                int numDrops = 1 + (int) ((currentAmp * 1000) / 50f);
                Mathf.Clamp(numDrops, 1, 3);
                Debug.Log("drops: " + numDrops);
                for(int i = 0; i < numDrops; i++)
                {
                    SpawnRaindrop();
                }

                time = 0;
            }

        }
        previousAmp = currentAmp;
    }

    void SpawnRaindrop() {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-xRange, xRange), height, UnityEngine.Random.Range(-zRange, zRange));
        GameObject drop = Instantiate(dropPrefab, position, Quaternion.identity);
        drop.GetComponent<Rigidbody>().AddForce(Vector3.down * forceAmount, ForceMode.Impulse);
    }

}

