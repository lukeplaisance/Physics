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

        private float Xmin = 0, Xmax = 100, Ymin = 0, Ymax = 100, Zmin = 0, Zmax = 100;

        public float flock = 1.0f;

        //resets the boid's position on start
        private void Start()
        {
            foreach (var p in Boids)
            {
                p.isPerching = false;
                p.Position = new Vector3(0, 40, 0);
            }
        }

        private void LateUpdate()
        {
            MoveBoidsToNewPosition();
            foreach (var b in Boids)
            {
                b.isPerching = false;
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
                        b.isPerching = false;
                    }
                }

                v1 = Bound_Position(b);
                v2 = flock * Boid_Cohesion(b);
                v3 = Boid_Dispersion(b);
                v4 = Boid_Alignment(b);


                b.Velocity = b.Velocity + v2 + v3 + v4 * Time.deltaTime;

                if (b.Velocity.magnitude > 5)
                    b.Velocity = b.Velocity.normalized;

                b.Position = b.Position + b.Velocity;
                gameObjects[Boids.IndexOf(b)].transform.position = b.Position;
            }
        }

        private void OnDrawGizmos()
        {
            var rect = new Rect(Xmin, Xmax, Ymin, Ymax);
            Gizmos.DrawCube(Vector3.zero, new Vector3(100, 100, 100));
        }
        public Vector3 Bound_Position(ParticleData b)
        {
            //min and max positions
            

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
                v.x = 0;
            }

            if (b.Position.y < Ymin)
            {
                v.y = 100;
            }
            else if (b.Position.x > Ymax)
            {
                v.y = 0;
            }

            if (b.Position.y < Zmin)
            {
                v.z = 100;
            }
            else if (b.Position.x > Zmax)
            {
                v.z = 0;
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
                if (item.isPerching)
                    continue;

                if (item != b)
                {
                    pc += item.Position;
                }
            }
            pc = pc / (N - 1);
            return (pc - b.Position) / 50;
        }

        public Vector3 Boid_Dispersion(ParticleData b)
        {
            //the displacement of each boid
            Vector3 c = Vector3.zero;

            //if the position of a boid is less thatn 5 units away from another, 
            //the boid will go the opposite way
            foreach (var item in Boids)
            {
                if (item.isPerching)
                    continue;

                if (item != b)
                    if ((item.Position - b.Position).magnitude <= 5)
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
                if (item.isPerching)
                    continue;

                if (item != b)
                {
                    pv += b.Velocity;
                }
            }
            pv = pv / (N - 1);
            return (pv - b.Velocity) / 8;
        }

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
    }
}

