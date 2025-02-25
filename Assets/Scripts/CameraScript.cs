using System.Collections;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    [SerializeField] private Vector3 pos1, pos2;
    [SerializeField] private Quaternion rot1, rot2;
    [SerializeField] private float duration;
    private bool movingToPos1 = false;
    private float startTime;
    private bool moving = false;

    private void Start()
    {
        startTime = Time.time;
        StartCoroutine(LerpRoutine());
    }

    private IEnumerator LerpRoutine()
    {
        yield return new WaitForSeconds(167 - duration);
        
        yield return StartCoroutine(LerpTransform(pos2, pos1, rot2, rot1, duration));
        
        yield return new WaitForSeconds(250 - (Time.time - startTime));
        yield return StartCoroutine(LerpTransform(pos1, pos2, rot1, rot2, duration));
    }

    private IEnumerator LerpTransform(Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, float duration)
    {
        moving = true;
        float elapsedTime = 0f;
        
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpFraction = Mathf.Sin((elapsedTime / duration) * Mathf.PI * 0.5f);
            transform.position = Vector3.Lerp(startPos, endPos, lerpFraction);
            transform.rotation = Quaternion.Slerp(startRot, endRot, lerpFraction);
            yield return null;
        }
        
        transform.position = endPos;
        transform.rotation = endRot;
        moving = false;
    }
}
