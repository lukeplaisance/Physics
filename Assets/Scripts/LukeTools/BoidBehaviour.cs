using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LukeTools
{
    public class BoidBehaviour : MonoBehaviour
    {
        [SerializeField]
        public List<ParticleData> Boids;
        public List<GameObject> gameObjects;

        public float flock = 1.0f;

        //resets the boid's position on start
        private void Start()
        {
            foreach (var p in Boids)
            {
                p.Position = new Vector3(0, Random.Range(5, 10), 0);
            }
        }

        private void LateUpdate()
        {
            MoveBoidsToNewPosition();
            foreach (var b in Boids)
            {
                Debug.DrawLine(b.Position, b.Position + b.Velocity, Color.green, Time.deltaTime);
            }
        }

        public void MoveBoidsToNewPosition()
        {
            //declare vectors for the 3 boid rules methods
            Vector3 v1;
            Vector3 v2;
            Vector3 v3;
            Vector3 v4;

            //calls the boid rules methods for each boid
            foreach (var b in Boids)
            {
                //checks to see if a boid is perching, if so, the perch timer starts
                if (b.isPerching)
                {
                    if (b.perch_timer > 0)
                    {
                        b.perch_timer = b.perch_timer - 1;
                    }
                    else
                    {
                        b.Position = new Vector3(0, 30, 0);
                        b.isPerching = false;
                        b.perch_timer = 50;
                    }
                }

                if (b.perch_timer == 50)
                {
                    v2 = flock * Boid_Cohesion(b);
                    v3 = Boid_Dispersion(b);
                    v4 = Boid_Alignment(b);
                    v1 = Bound_Position(b);


                    b.Velocity = b.Velocity + v2 + v3 + v4 * Time.deltaTime;

                    if (b.Velocity.magnitude > 10)
                        b.Velocity = b.Velocity.normalized;

                    b.Position = b.Position + b.Velocity;
                    gameObjects[Boids.IndexOf(b)].transform.position = b.Position;
                }
            }
        }

        public Vector3 Bound_Position(ParticleData b)
        {
            //min and max positions
            float Xmin = -100, Xmax = 100, Ymin = 1, Ymax = 100, Zmin = -100, Zmax = 100;

            float groundLevel = 0;

            Vector3 v = Vector3.zero;

            if (b.Position.y < groundLevel)
            {
                b.Position.y = groundLevel;
                b.isPerching = true;
            }

            if (b.Position.x < Xmin)
            {
                v.x = 100;
            }
            else if (b.Position.x > Xmax)
            {
                v.x = -100;
            }

            if (b.Position.y < Ymin)
            {
                v.y = 0;
            }
            else if (b.Position.x > Ymax)
            {
                v.y = -100;
            }

            if (b.Position.y < Zmin)
            {
                v.z = 100;
            }
            else if (b.Position.x > Zmax)
            {
                v.z = -100;
            }

            return v;
        }

        public Vector3 Boid_Cohesion(ParticleData b)
        {
            //the number of boids
            var N = Boids.Count;

            //the perceived center
            Vector3 pc = Vector3.zero;

            //finds the average position of each boid
            foreach (var item in Boids)
            {
                if (item != b)
                {
                    pc += item.Position;
                }
            }
            pc = pc / (N - 1);
            return (pc - b.Position) / 100;
        }

        public Vector3 Boid_Dispersion(ParticleData b)
        {
            //the displacement of each boid
            Vector3 c = Vector3.zero;

            //if the position of a boid is less thatn 5 units away from another, 
            //the boid will go the opposite way
            foreach (var item in Boids)
            {
                if (item != b)
                    if ((item.Position - b.Position).magnitude <= 15)
                    {
                        c = c - (item.Position - b.Position);
                    }
            }
            return c;
        }

        public Vector3 Boid_Alignment(ParticleData b)
        {
            //the number of boids
            var N = Boids.Count;

            //percieved velocity
            Vector3 pv = Vector3.zero;

            //finds the average velocity of each boid
            foreach (var item in Boids)
            {
                if (item != b)
                {
                    pv += b.Velocity;
                }
            }
            pv = pv / (N - 1);
            return (pv - b.Velocity) / 8;
        }
#if UNITY_EDITOR
        [CustomEditor(typeof(BoidBehaviour))]
        public class BoidBehaviourEditor : Editor
        {
            private BoidBehaviour b;

            private void OnEnable()
            {
                b = target as BoidBehaviour;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                b.flock = GUILayout.HorizontalSlider(b.flock, 1, -1);

                if (GUILayout.Button("Land Boids"))
                {
                    foreach (var item in b.Boids)
                    {
                        item.Position.y = 0;
                    }
                }
            }
        }
#endif
    }
}

