using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LukeTools
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [SerializeField]
        public ProjectileData pd;

        // Use this for initialization
        void Start()
        {
            pd = new ProjectileData();
        }

        public Vector3 ProjectileMove(Vector3 velocity, float angle, Vector3 initial_height)
        {
            pd.time = Time.deltaTime;
            pd.current_velocity.x = pd.initial_velocity.x * Mathf.Cos(angle);
            pd.current_velocity.y = pd.initial_velocity.y * Mathf.Sin(angle);

            //calulate the magnitude of the velocity
            float velocity_mag = (pd.current_velocity - pd.initial_velocity).magnitude;

            //calculate the speed
            pd.speed = velocity_mag / (2.0f * pd.gravity).magnitude;
            velocity_mag = 2.0f * pd.gravity.magnitude * pd.speed;

            //calculate the current position
            pd.current_position.x = pd.initial_position.x + (pd.initial_velocity.x * pd.time);
            pd.current_position.y = pd.initial_position.y + (pd.initial_velocity.y * pd.time) +
                                  (1 / 2 * (pd.gravity.magnitude * pd.time));

            return pd.current_position;
        }
    }
}
