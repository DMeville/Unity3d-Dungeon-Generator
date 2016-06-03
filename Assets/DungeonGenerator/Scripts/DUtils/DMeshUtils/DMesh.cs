using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class DMesh : MonoBehaviour {
    //this should simple be a container to easily create meshes without having to worry so much about setting triangles and stupid stuff
    public Mesh mesh;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private DMeshData data = new DMeshData();

    public void Awake() {
        mesh = new Mesh();
        meshFilter = this.GetComponent<MeshFilter>();
        meshRenderer = this.GetComponent<MeshRenderer>();
        meshFilter.mesh = mesh;
    }

    public void Apply(bool calculateNormals = false) {
        mesh.vertices = data.vertices.ToArray();
        mesh.triangles = data.triangles.ToArray();
        mesh.uv = data.uvs.ToArray();
        mesh.colors32 = data.colors.ToArray();
        mesh.RecalculateBounds();


        if (calculateNormals) {
            RecalculateNormals();
        }

        mesh.normals = data.normals.ToArray();
    }

    public void AssignColorsFromMesh() {
        List<Color32> col = new List<Color32>();
        for (int i = 0; i < mesh.colors.Length; i++) {
            col.Add(mesh.colors[i]);
        }
        data.colors = col;
    }

    public void RecalculateNormals() {
        List<int> tris = this.data.triangles;
        Vector3[] normals = new Vector3[this.data.vertices.Count];
        for (int i = 0; i < tris.Count; i += 3) {
            Vector3 a = this.data.vertices[tris[i]] - this.data.vertices[tris[i + 1]];
            Vector3 b = this.data.vertices[tris[i]] - this.data.vertices[tris[i + 2]];

            Vector3 normal = Vector3.Cross(a, b);
            normals[tris[i]] += normal;
            normals[tris[i+1]] += normal;
            normals[tris[i+2]] += normal;
        }

        List<Vector3> nList = new List<Vector3>();
        for (int i = 0; i < normals.Length; i++) {
            nList.Add(normals[i].normalized);
        }
        SetNormals(nList);
    }
  

    public void SetData(DMeshData _data) {
        this.data = _data;
    }

    public DMeshData GetData() {
        return this.data;
    }

    public void SetVerticies(List<Vector3> _vertices){
        this.data.vertices = _vertices;
    }

    public void SetNormals(List<Vector3> _normals) {
        this.data.normals = _normals;
    }

    public void SetTriangles(List<int> _triangles) {
        this.data.triangles = _triangles;
    }

    public void SetUvs(List<Vector2> _uvs) {
        this.data.uvs = _uvs;
    }

    public void SetColors(List<Color32> _colors) {
        this.data.colors = _colors;
    }


    public static string MeshToString(MeshFilter mf) {
        Mesh m = mf.mesh;
        Material[] mats = mf.GetComponent<Renderer>().sharedMaterials;

        StringBuilder sb = new StringBuilder();

        sb.Append("g ").Append(mf.name).Append("\n");
        foreach (Vector3 v in m.vertices) {
            sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");
        foreach (Vector3 v in m.normals) {
            sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");
        foreach (Vector3 v in m.uv) {
            sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
        }
        for (int material = 0; material < m.subMeshCount; material++) {
            sb.Append("\n");
            sb.Append("usemtl ").Append(mats[material].name).Append("\n");
            sb.Append("usemap ").Append(mats[material].name).Append("\n");

            int[] triangles = m.GetTriangles(material);
            for (int i = 0; i < triangles.Length; i += 3) {
                sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                    triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
            }
        }
        return sb.ToString();
    }


}

