using Code.Data;
using Data;
using UnityEngine;

namespace Code.Actor.Rocket
{
    public class FuelGuageController : MonoBehaviour
    {
        [SerializeField] private IntReference amountOfFuel;
        [SerializeField] private int maxGaugeValue;
        public GameObject needle;
        private float needleWidthDefault;
        private float needleXPos;
        private RectTransform needleRectTransform;
        private void Start()
        {
            needleRectTransform = needle.GetComponent<RectTransform>();
            needleWidthDefault = needle.transform.localScale.x;
            needleXPos = needle.transform.position.x;
        }

        public void UpdateGauge()
        {
            if (amountOfFuel.Value > maxGaugeValue) return;

            var currentFuelLevel = (float) amountOfFuel.Value;

            needle.transform.localScale = new Vector2(needleWidthDefault * (currentFuelLevel + 1f), 1f);
            needle.transform.position = new Vector3(
                needleXPos + 0.25f*needleWidthDefault * currentFuelLevel ,
                needle.transform.position.y, 0f);
            
        }
    }
}
