using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LukeTools;

namespace Cloth
{
    public class SliderBehavior : MonoBehaviour
    {
        public ClothGeneratorBehavior cg;
        public Slider gravitySlider;
        public Slider ksSlider;
        public Slider kdSlider;
        public Slider forceSlider;

        void Update()
        {
            foreach(var p in cg.Particles)
            {
                p.gravity.y = gravitySlider.value;
            }
            foreach(var s in cg.Springs)
            {
                s.ks = ksSlider.value;
                s.kd = kdSlider.value;
            }
            foreach(var a in cg.Triangles)
            {
                a.aeroForce.z = forceSlider.value;
            }
        }
    }
}
