using Unity.VisualScripting;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Vector3 pos1, pos2;
    [SerializeField] private Quaternion rot1, rot2;
    private bool moving = false, moved = false;

    private float time = 0f;
    [SerializeField] private float duration;
    [SerializeField] Transform transform;

    private void OnSwitch()
    {
        moving = true;
        time = 0f;
    }
    
    void Update()
    {
        time += Time.deltaTime;

        if(time >= 167 - duration && !moved)
        {
            moved = true;
            OnSwitch();
        }
        if (moving)
        {
            time += Time.deltaTime;
            float lerpFraction = Mathf.Sin((time / duration) * Mathf.PI * 0.5f); // Sine-like easing
            
            transform.position = Vector3.Lerp(pos2, pos1, lerpFraction);
            transform.rotation = Quaternion.Lerp(rot2, rot1, lerpFraction);


            if (time >= duration)
            {
                // transform.position = pos1;
                // transform.rotation = rot1;
                moving = false;
            }
        }
    }

}
