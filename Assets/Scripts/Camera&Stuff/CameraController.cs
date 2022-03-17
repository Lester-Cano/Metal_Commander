using System;
using UnityEngine;
using DG.Tweening;

namespace Camera_Stuff
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] public Camera cam;
        [SerializeField] public Vector3 newDirection;
        [SerializeField] public Vector3 maxValue, minValue;

        public void PanCamera(string direction)
        {
            if (direction == "Up")
            {
                newDirection = Vector3.up;
                cam.transform.position += newDirection;
                ReBound();
            }
            else if (direction == "Down")
            {
                newDirection = Vector3.down;
                cam.transform.position += newDirection;
                ReBound();
            }
            else if (direction == "Right")
            {
                newDirection = Vector3.right;
                cam.transform.position += newDirection;
                ReBound();
            }
            else if (direction == "Left")
            {
                newDirection = Vector3.left;
                cam.transform.position += newDirection;
                ReBound();
            }
        }

        public void ReBound()
        {
            Debug.Log("rebounding");
            
            Vector3 targetPosition = cam.transform.position;
            Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x)
                , Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y)
                , Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));

            cam.transform.position = boundPosition;
        }
    }
}
