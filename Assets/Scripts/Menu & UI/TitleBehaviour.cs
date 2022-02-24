using System.Collections;
using UnityEngine;

namespace Menu___UI
{
    public class TitleBehaviour : MonoBehaviour
    {
        public void SetTitle(GameObject title)
        {
            Debug.Log("Setting title");
            title.SetActive(true);
        }
        
        public void RemoveTitle(GameObject title)
        {
            Debug.Log("Removing title");
            title.SetActive(false);
        }
    }
}
