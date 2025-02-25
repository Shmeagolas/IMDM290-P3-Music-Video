using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    public GameObject ripplePrefab;
    private List<GameObject> activeRipples = new List<GameObject>();
    private List<float> rippleTimes = new List<float>();

    void Update()
    {
        for (int i = 0; i < activeRipples.Count; i++) {
            GameObject ripple = activeRipples[i];

            rippleTimes[i] += Time.deltaTime;
            float elapsedTime = rippleTimes[i];

            Material water = ripple.GetComponent<Renderer>().material;

            Color newColor = water.color;
            newColor.a = Mathf.SmoothStep(1f, 0f, Mathf.Clamp01(elapsedTime / 3f));
            water.color = newColor;

            float expansionRatio = 1f - newColor.a;
            ripple.transform.localScale = new Vector3(expansionRatio * 2f, 0.0001f, expansionRatio * 2f);

            if (newColor.a <= 0f) {
                Destroy(ripple);
                activeRipples.RemoveAt(i);
                rippleTimes.RemoveAt(i);
                i--;
            }
        }
    }


    public void CreateRipple(Vector3 position) {
        GameObject newRipple = Instantiate(ripplePrefab, position, Quaternion.identity);

        Material water = newRipple.GetComponent<Renderer>().material;
        water.renderQueue = 3001;
        
        activeRipples.Add(newRipple);
        rippleTimes.Add(0f);
    }
}
