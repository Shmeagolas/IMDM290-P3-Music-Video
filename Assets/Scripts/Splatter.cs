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
            Vector3 pos = contact.point;
            ripple.CreateRipple(pos);
            Destroy(gameObject);
        }
    }
}
