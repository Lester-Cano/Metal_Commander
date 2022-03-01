using System.Collections;
using UnityEngine;

namespace Menu___UI
{
    public class TitleBehaviour : MonoBehaviour
    {
        public void SetTitle(GameObject title)
        {
            title.SetActive(true);
        }
        
        public void RemoveTitle(GameObject title)
        {
            title.SetActive(false);
        }
    }
}
