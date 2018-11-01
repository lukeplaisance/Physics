using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu (menuName = "ProjectileMovement")]
public class ProjectileMovement : ScriptableObject
{
    public Vector2 initial_velocity;
    public Vector2 current_velocity;

    public Vector2 initial_position;
    public Vector2 current_position;

    public Vector2 velocity;
    public float angle;
    public float height;

    private float mass;
    private float accel;
    private float gravity;
    private float time;

    public Vector2 ProjectileMove(Vector2 velocity, float angle, float height)
    {
        gravity = (mass * accel);
        time = Time.deltaTime;

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
