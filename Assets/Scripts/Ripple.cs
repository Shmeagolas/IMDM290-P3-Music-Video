using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    public GameObject ripplePrefab;
    public float expansionTime = 3f;
    private List<GameObject> activeRipples = new List<GameObject>();
    private List<float> rippleTimes = new List<float>();

    void Update()
    {
        for (int i = 0; i < activeRipples.Count; i++) {
            GameObject ripple = activeRipples[i];
            float elapsedTime = rippleTimes[i];

            elapsedTime += Time.deltaTime;

            float expansionRatio = Mathf.Min(elapsedTime / expansionTime, 1f);
            ripple.transform.localScale = new Vector3(expansionRatio, 0, expansionRatio);

            if (elapsedTime >= expansionTime) {
                Destroy(ripple);
                activeRipples.RemoveAt(i);
                rippleTimes.RemoveAt(i);
                i--;
            } else {
                rippleTimes[i] = elapsedTime;
            }
        }
    }

    public void CreateRipple(Vector3 position) {
        GameObject newRipple = Instantiate(ripplePrefab, position, Quaternion.identity);
        activeRipples.Add(newRipple);
        rippleTimes.Add(0f);
    }
}
