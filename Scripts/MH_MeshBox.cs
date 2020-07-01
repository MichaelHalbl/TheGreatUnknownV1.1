using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MH_MeshBox : MonoBehaviour
{

    Mesh mesh;
    List<Vector2> uvs;

    List<Vector3> vertices;
    List<int> triangles;
    List<Vector3> normals;
    int index;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = new List<Vector3>();
        triangles = new List<int>();
        uvs = new List<Vector2>();
        normals = new List<Vector3>();
    }

    void Start()
    {
        CreateCube();
        build();
    }

    private void CreateCube()
    {
        Vector3 a = new Vector3(0, 0, 0);
        Vector3 b = new Vector3(1, 0, 0);
        Vector3 c = new Vector3(1, 1, 0);

        Vector3 d = new Vector3(0, 1, 0);
        Vector3 e = new Vector3(0, 1, 1);
        Vector3 f = new Vector3(1, 1, 1);
        Vector3 g = new Vector3(1, 0, 1);
        Vector3 h = new Vector3(0, 0, 1);


        Face(a, b, c, d);  //Front
        //Face(d, c, b, a);


        Face(g, h, e, f); //back


        Face(h, a, d, e);
        Face(b, g, f, c);
        Face(c, f, g, b);  //site
        Face(e, d, a, h);

        Face(d, c, f, e); //dach

        Face(h, g, b, a); // boden 
        Face(g, h, a, b);
    }
    private void Face(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        Vector3 normal = GetNormal(a, b, c);

        vertices.Add(a); normals.Add(normal); uvs.Add(new Vector2(0f, 0f));
        vertices.Add(b); normals.Add(normal); uvs.Add(new Vector2(1f, 0f));
        vertices.Add(c); normals.Add(normal); uvs.Add(new Vector2(1f, 1f));
        vertices.Add(d); normals.Add(normal); uvs.Add(new Vector2(0f, 1f));

        vertices.Add(a); normals.Add(normal); uvs.Add(new Vector2(0f, 0f));
        vertices.Add(c); normals.Add(normal); uvs.Add(new Vector2(1f, 1f));



        triangles.Add(index + 2);
        triangles.Add(index + 1);
        triangles.Add(index + 0);

        triangles.Add(index + 4);
        triangles.Add(index + 3);
        triangles.Add(index + 5);

        index = index + 6;
    }

    private Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c)
    {

        Vector3 E1 = c - a;
        Vector3 E2 = b - a;

        return Vector3.Cross(E1, E2).normalized;
    }
    void build()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
}
