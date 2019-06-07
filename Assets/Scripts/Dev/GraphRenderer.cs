using Assets.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Dev
{
    public class GraphRenderer : MonoBehaviour
    {
        public DecisionHandler DecisionHandler;
        public GameObject LineRenderObject;
        public Slider[] Sliders = new Slider[3];
        public GameObject[] Points = new GameObject[3];
        public Text BehaviourText;

        private float _scale = 10f;
        private float _xOffset = -15f;

        public void InitializeGraphs()
        {
            var sets = DecisionHandler.Sets;
            var list = sets.GetGraphPoints();

            var yOffset = 10;
            foreach (var s in list)
            {
                foreach (var ss in s)
                {
                    var obj = Instantiate(LineRenderObject);
                    obj.transform.SetParent(transform);
                    var lineRenderer = obj.GetComponent<LineRenderer>();
                    lineRenderer.positionCount = 4;
                    lineRenderer.SetPositions(new Vector3[]
                    {
                        new Vector3(ss[0] / _scale + _xOffset, 0 + yOffset, 19),
                        new Vector3(ss[1] / _scale + _xOffset, 2 + yOffset, 19),
                        new Vector3(ss[2] / _scale + _xOffset, 2 + yOffset, 19),
                        new Vector3(ss[3] / _scale + _xOffset, 0 + yOffset, 19)
                    });
                }
                yOffset -= 8;
            }
        }

        public void UpdateSliders(int sliderID)
        {
            var setValues = DecisionHandler.GetSetValues(new float[] {
                Sliders[0].value, Sliders[1].value, Sliders[2].value});

            var terms = new string[] { "Health", "Courage", "Angle" };

            setValues.TryGetValue(terms[sliderID] + "_LOW", out float yValue1);
            setValues.TryGetValue(terms[sliderID] + "_MEDIUM", out float yValue2);
            setValues.TryGetValue(terms[sliderID] + "_HIGH", out float yValue3);

            var y = 10 - (sliderID * 8) + Mathf.Max(yValue1, yValue2, yValue3) * 2;
            var x = Sliders[sliderID].value / _scale + _xOffset;
            Points[sliderID].transform.position = new Vector3(x, y, 19);

            BehaviourText.text = DecisionHandler.GetPreferredAction(setValues);
        }
    }
}
