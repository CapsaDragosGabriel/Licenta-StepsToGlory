
using UnityEngine;
using UnityEngine.Rendering;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void LateUpdate()
    {
        if (target != null)
        {
    
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothPos= Vector3.Lerp(transform.position, desiredPos, smoothSpeed); 
            transform.position = smoothPos;

        }else
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        }
    }


}