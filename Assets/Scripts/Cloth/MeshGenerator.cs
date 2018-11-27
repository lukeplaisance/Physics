using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class MeshGenerator : MonoBehaviour
    {
        public MeshFilter InstanceMeshFilter;
        public Mesh InstanceMesh;

        private List<Vector3> Vertices = new List<Vector3>();
        private List<int> TrianglePoints = new List<int>();
        private List<Vector3> SurfaceNormals = new List<Vector3>();
        private List<Vector2> UVs = new List<Vector2>();



        public void Awake()
        {
            InstanceMesh = new Mesh();
            InstanceMesh.name = "Mesh";
            //creates and adds new verts to the Vertices list
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    Vertices.Add(new Vector3(x, y, 0));
                }
            }
            //assign the list converted to an array to the verticies array inside the InstanceMesh variable
            InstanceMesh.vertices = Vertices.ToArray();

            //create the triangles
            for (int i = 0; i < Vertices.Count; i++)
            {
                //If we are not on the edge of the verts we will create  triangle
                if (i % 5 != 5 - 1 && i < Vertices.Count - 5)
                {
                    //Bot Triangle
                    TrianglePoints.Add(i); //bot left
                    TrianglePoints.Add(i + 1); //bot right
                    TrianglePoints.Add(i + 5); //top Left

                    //Top Trianlge
                    TrianglePoints.Add(i + 1); //bot right
                    TrianglePoints.Add(i + 5 + 1); //top right
                    TrianglePoints.Add(i + 5); //top left
                }
            }
            InstanceMesh.triangles = TrianglePoints.ToArray();

            //set the surface normals
            foreach (var vert in Vertices)
            {
                SurfaceNormals.Add(new Vector3(0, 0, 1));
            }
            InstanceMesh.normals = SurfaceNormals.ToArray();

            //set the UVs
            foreach (var vert in Vertices)
            {
                //We set the x and y of each UV to be the x and y values of the vert's posititon divided by the width of the verts - 1
                UVs.Add(new Vector2(vert.x / (5 - 1), vert.y / (5 - 1)));
            }
            InstanceMesh.uv = UVs.ToArray();

            InstanceMeshFilter.mesh = InstanceMesh;
        }
    }
}
