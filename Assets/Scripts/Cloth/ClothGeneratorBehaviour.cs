using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;
using UnityEditor;

namespace Cloth
{
    public class ClothGeneratorBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<Particle> Particles = new List<Particle>();
        List<GameObject> gameObjects = new List<GameObject>();
        private List<SpringDamper> Springs = new List<SpringDamper>();
        public List<AeroDynamicForce> Triangles = new List<AeroDynamicForce>();
        public float width;
        public float height;


        void Awake()
        {
            for (float x = 0; x < width; x++)
            {
                for (float y = 0; y < height; y++)
                {
                    Particles.Add(new Particle(new Vector3(x, y, 0)));
                    var newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    newObj.transform.position = Particles[Particles.Count - 1].Position;
                    gameObjects.Add(newObj);
                }
            }

            for (int i = 0; i < Particles.Count; i++)
            {
                if (i % width != width - 1)
                {
                    Springs.Add(new SpringDamper(Particles[i], Particles[i + 1]));
                }
                if (i < Particles.Count - width)
                {
                    Springs.Add(new SpringDamper(Particles[i], Particles[i + (int)width]));
                }

                if (i % width != width - 1 && i < Particles.Count - width)
                {
                    Springs.Add(new SpringDamper(Particles[i], Particles[i + (int)width + 1]));
                }
                if (i % width != 0 && i < Particles.Count - height)
                {
                    Springs.Add(new SpringDamper(Particles[i], Particles[i + (int)width - 1]));
                }
            }

            //adding the cross springs
            for (int i = 0; i < Particles.Count; i++)
            {
                if (i % width != width - 1 && i < Particles.Count - width)
                {
                    Triangles.Add(new AeroDynamicForce(Particles[i], Particles[i + 1], Particles[i + (int)width]));
                    Triangles.Add(new AeroDynamicForce(Particles[i + 1], Particles[i + (int)width + 1], Particles[i + (int)width]));
                }
            }

            foreach (var particle in Particles)
            {
                if (particle.Position.y == height - 1)
                {
                    particle.isAnchor = true;
                }
            }
        }
        void OnDrawGizmos()
        {
            foreach (var p in Particles)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(p.Position, .25f);

                if(p.isGrabbed)
                {
                    Gizmos.color = Color.blue;
                }
            }
            foreach (var s in Springs)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(s.p1.Position, s.p2.Position);
            }
        }

        // Update is called once per frame
        void Update()
        {
            var mousePos = Input.mousePosition;
            Debug.Log(mousePos);

            

            for(int i = 0; i < Particles.Count; i++)
            {
                if (Particles[i].isGrabbed && Input.GetKey(KeyCode.A))
                {
                    Particles[i].isActive = false;
                    Particles.Remove(Particles[i]);
                }
            }


            foreach (var s in Springs)
            {
                s.Update();
            }
            foreach (var force in Triangles)
            {
                force.Update();
            }
            foreach (var particle in Particles)
            {
                var gravity = new Vector3(0, -9.81f, 0);
                particle.AddForce(gravity * .25f);
                particle.Update(Time.deltaTime);                
            }

            for(int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].transform.position = Particles[i].Position;
            }
        }
    }
}