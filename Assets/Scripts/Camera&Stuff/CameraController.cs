using UnityEngine;
using UnityEngine.Tilemaps;

namespace Camera_Stuff
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] public Camera cam;
        [SerializeField] public Vector3 newDirection;

        [SerializeField] private Tilemap mapLimit;

        public void PanCamera(string direction)
        {
            if (direction == "Up")
            {
                newDirection = Vector3.up;
                cam.transform.position += newDirection;
            }
            else if (direction == "Down")
            {
                newDirection = Vector3.down;
                cam.transform.position += newDirection;
            }
            else if (direction == "Right")
            {
                newDirection = Vector3.right;
                cam.transform.position += newDirection;
            }
            else if (direction == "Left")
            {
                newDirection = Vector3.left;
                cam.transform.position += newDirection;
            }
        }
    }
}
