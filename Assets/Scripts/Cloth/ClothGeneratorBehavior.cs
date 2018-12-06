using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LukeTools;
using UnityEditor;

namespace Cloth
{
    public class ClothGeneratorBehavior : MonoBehaviour
    {
        public List<Particle> Particles = new List<Particle>();
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<SpringDamper> Springs = new List<SpringDamper>();
        public List<AeroDynamicForce> Triangles = new List<AeroDynamicForce>();

        private SpringDamper spring;
        public float width;
        public float height;
        private Particle grabbed;
        private Vector3 worldMouse;

        void Awake()
        {
            GenCloth();
        }

        public void GenCloth()
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

            //adding the spring dampers
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

            worldMouse = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 
                                                        -Camera.main.transform.position.z));

            if(Input.GetMouseButtonDown(0))
            {
                foreach(var p in Particles)
                {
                    var scalePosition = new Vector3(p.Position.x * transform.localScale.x, 
                                                    p.Position.y * transform.localScale.y, 
                                                    p.Position.z * transform.localScale.z);
                    var checkPos = new Vector3(worldMouse.x, worldMouse.y, p.Position.z);
                    if (Vector3.Distance(checkPos, scalePosition) <= 1f)
                        grabbed = p;
                }
            }

            if(Input.GetMouseButton(0) && grabbed != null)
            {
                grabbed.Position = worldMouse;
                if(grabbed.Force.magnitude >= 5 || Input.GetKeyDown(KeyCode.A))
                {
                    grabbed.isActive = true;
                    for(var i = 0; i < Springs.Count; i++)
                    {
                        if(Springs[i].CheckParticles(grabbed))
                        {
                            Springs.RemoveAt(i);
                        }
                    }
                    for (var i = 0; i < Triangles.Count; i++)
                    {
                        if(Triangles[i].CheckParticles(grabbed))
                        {
                            Triangles.RemoveAt(i);
                        }
                    }
                    if (Input.GetKeyDown(KeyCode.A))
                        grabbed.isAnchor = !grabbed.isAnchor;
                }
                if (Input.GetMouseButtonUp(0))
                    grabbed = null;

               
            }
        }

        private void LateUpdate()
        {
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

            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].transform.position = Particles[i].Position;
                gameObjects[i].transform.localScale = new Vector3(.2f, .2f, .2f);
                Destroy(gameObjects[i].GetComponent("SphereCollider"));
            }
        }
    }
}