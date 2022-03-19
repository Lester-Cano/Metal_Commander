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
        
        //From here another try

        private Vector3 origin, difference, resetCamera;
        private bool drag;
        [SerializeField] private Camera mainCamera;

        private void Start()
        {
            resetCamera = cam.transform.position;
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                difference = (cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position);
                if (drag == false)
                {
                    drag = true;
                    origin = cam.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                drag = false;
            }

            if (drag)
            {
                cam.transform.position = origin - difference;
                ReBound();
            }

            if (Input.GetMouseButton(1))
            {
                cam.transform.position = resetCamera;
            }
        }

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
            Vector3 targetPosition = cam.transform.position;
            Vector3 boundPosition = new Vector3(Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x)
                , Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y)
                , Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));

            cam.transform.position = boundPosition;
        }
    }
}
