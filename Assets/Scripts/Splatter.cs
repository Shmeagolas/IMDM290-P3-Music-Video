using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Splatter : MonoBehaviour
{
    GameObject rippleManager;
    private Ripple ripple;

    void Start()
    {
        rippleManager = GameObject.Find("RippleManager");
        ripple = rippleManager.GetComponent<Ripple>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Plane") || collision.gameObject.CompareTag("Ripple")) {
            ContactPoint contact = collision.contacts[0];
            Vector3 pos = contact.point + new Vector3(0f, .1f, 0);
            //Destroy(gameObject);
            StartCoroutine(CreateRipplesWithDelay(pos));
        }
    }

    void Update()
    {
        Vector3 pos = this.transform.position;
        if (pos.y <= 0)
        {

            StartCoroutine(CreateRipplesWithDelay(new Vector3(pos.x, .1f, pos.z)));
        }
    }

    IEnumerator CreateRipplesWithDelay(Vector3 position)
    {
        // Create the first ripple immediately
        ripple.CreateRipple(position);
        Debug.Log("First ripple created");

        // Wait for a short delay before creating the second ripple
        yield return new WaitForSeconds(0.3f); // Adjust the delay time as needed
        ripple.CreateRipple(position);
        Debug.Log("2nd ripple created");

        // Wait for another short delay before creating the third ripple
        yield return new WaitForSeconds(0.3f); // Adjust the delay time as needed
        ripple.CreateRipple(position);
        Debug.Log("3rd ripple created");
        Destroy(gameObject);

    }


}
