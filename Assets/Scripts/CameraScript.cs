using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private Vector3 pos1, pos2;
    [SerializeField] private Quaternion rot1, rot2;
    private bool moving = false;

    private float time = 0f;
    [SerializeField] private float duration;

    private void OnSwitch()
    {
        moving = true;
        time = 0f;  
    }
    void Start()
    {
            Vector3 tempPos = pos1;
            pos1 = pos2;
            pos2 = tempPos;

            Quaternion tempRot = rot1;
            rot1 = rot2;
            rot2 = tempRot;
    }
    void Update()
    {
        time += Time.deltaTime;

        if (time >= 167 - duration && !moving)
        {
            OnSwitch();
        }

        if (time >= 250 && moving)
        {
            Vector3 tempPos = pos1;
            pos1 = pos2;
            pos2 = tempPos;

            Quaternion tempRot = rot1;
            rot1 = rot2;
            rot2 = tempRot;

            OnSwitch();
        }

        if (moving)
        {
            float lerpFraction = Mathf.Sin((time / duration) * Mathf.PI * 0.5f);
            transform.position = Vector3.Lerp(pos1, pos2, lerpFraction);
            transform.rotation = Quaternion.Lerp(rot1, rot2, lerpFraction);

            if (time >= duration)
            {
                moving = false;
            }
        }
    }
}
