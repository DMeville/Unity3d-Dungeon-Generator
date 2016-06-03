using System.Collections.Generic;
using UnityEngine;

public class DMeshData {

    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    public List<Vector2> uvs = new List<Vector2>();
    public List<Vector3> normals = new List<Vector3>();
    public List<Color32> colors = new List<Color32>();

    public static DMeshData GeneratePlane(int width, int height, Vector3 normalDirection){
        DMeshData data = new DMeshData();

        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                verts.Add(new Vector3(j,0f, i));
                uv.Add(new Vector2(j, i));
            }
        }

        data.vertices = verts;
        data.uvs = uv;

        data.triangles = new List<int>();
        for (int i = 0; i < width-1; i++) {
            for (int j = 0; j < height-1; j++) {
                data.triangles.Add(((i + 1) * height) + (j));        //2 BOTTOM LEFT
                data.triangles.Add(((i) * height) + (j + 1));        //1 TOP RIGHT
                data.triangles.Add(((i) * height) + (j));            //0 TOP LEFT

                data.triangles.Add(((i + 1) * height) + (j));        //2
                data.triangles.Add(((i + 1) * height) + (j + 1));    //3 BOTTOM RIGHT           
                data.triangles.Add(((i) * height) + (j + 1));        //1
            }
        }

        List<Vector3> normals = new List<Vector3>();
        for (int i = 0; i < verts.Count; i++) {
            normals.Add(normalDirection);
        }
        data.normals = normals;
        return data;
    }

    public static DMeshData GeneratePlane(int width, int height) {
        return GeneratePlane(width, height, Vector3.up);
    }

    internal static DMeshData GeneratePlane(float width, float height) {
        return GeneratePlane((int)width, (int)height, Vector3.up);
    }
}
