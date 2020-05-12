using UnityEngine;

namespace Code
{
    public class ApplicationConfig : MonoBehaviour
    {
        public int targetFrameRate = 60;
        // Start is called before the first frame update
        private void Awake()
        {
            // QualitySettings.vSyncCount = 0;
            // Application.targetFrameRate = targetFrameRate;
        }

        // // Update is called once per frame
        // void Update()
        // {
        //
        // }
    }
}
