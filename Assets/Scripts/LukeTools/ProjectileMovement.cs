using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu (menuName = "ProjectileMovement")]
public class ProjectileMovement : ScriptableObject
{
    public Vector3 initial_velocity;
    public Vector3 current_velocity;

    public Vector3 initial_position;
    public Vector3 current_position;

    public Vector3 velocity;
    public Vector3 accel;
    public Vector3 initial_height;
    public Vector3 current_height;
    public Vector3 gravity = new Vector3(0, -9.81f, 0);

    private float mass;
    public float angle;
    private float time;
    private float speed;

    public Vector2 ProjectileMove(Vector2 velocity, float angle, Vector2 initial_height)
    {
        float velocity_mag = (current_velocity - initial_velocity).magnitude;
        time = Time.deltaTime;
        speed = velocity_mag / (accel * 2.0f).magnitude;
        velocity_mag = 2.0f * accel.magnitude * speed;
        


        current_position.x = initial_position.x + (initial_velocity.x * time);
        current_position.y = initial_position.y + (initial_velocity.y * time) +
                              (1 / 2 * Mathf.Sqrt(gravity * time));

        return current_position;
    }
	
}


[CustomEditor(typeof(ProjectileMovement))]
public class ProjectileMovementEditor : Editor
{
    private ProjectileMovement pm;
    
    public override void OnInspectorGUI()
    {
        pm = (ProjectileMovement)target;
        


        if(GUILayout.Button("Calculate"))
        {
            pm.ProjectileMove(pm.initial_velocity, pm.angle, pm.height);
        }
        GUILayout.Box(" End Position " + pm.current_position.ToString());
    }
}
