using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianterLeiDa : MonoBehaviour
{


    List<Vector3> outPoints;

    List<Vector3> inerPoints;

    int edgeCount = 5;

    float radius = 1;
    // Use this for initialization
    void Start()
    {

        outPoints = new List<Vector3>();

        inerPoints = new List<Vector3>();

        for (int i = 0; i < edgeCount; i++)
        {
            float a = i / (float)edgeCount;
            float angle = 72 + a * Mathf.PI * 2;

            Vector3 tmp = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);

            outPoints.Add(tmp);

        }

        for (int i = 0; i < edgeCount; i++)
        {
            Vector3 tmp = outPoints[i] * Random.Range(0.1f, 1);

            inerPoints.Add(tmp);
        }



    }

    static Material lineMaterial;
    static void CreateLineMaterial()
    {
        if (!lineMaterial)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;
            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }


    public void DrawOutPoints()
    {
        GL.Begin(GL.LINES);

        for (int i = 0; i < outPoints.Count; i++)
        {
            GL.Vertex(Vector3.zero);

            GL.Vertex(outPoints[i]);

        }

        for (int i = 1; i < outPoints.Count + 1; i++)
        {


            GL.Vertex(outPoints[i - 1]);

            int tmpIndex = i % outPoints.Count;
            GL.Vertex(outPoints[tmpIndex]);

        }



        GL.End();


    }

    public void DrawInnerPoints()
    {
        GL.Begin(GL.TRIANGLES);

        for (int i = 0; i < inerPoints.Count; i++)
        {

            Color tmpColor = new Color(41 / 255.0f, 88 / 255.0f, 93 / 255.0f, 0.9f);

            GL.Color(tmpColor);
            GL.Vertex(Vector3.zero);

            Vector3 second = inerPoints[i];


            GL.Color(tmpColor * 0.5f);
            GL.Vertex(second);
            int tmpIndex = (i + 1) % inerPoints.Count;


            GL.Color(tmpColor * 0.1f);
            Vector3 three = inerPoints[tmpIndex];

            GL.Vertex(three);




        }


        GL.End();

    }

    // Will be called after all regular rendering is done
    public void OnRenderObject()
    {
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        // GL.MultMatrix(transform.localToWorldMatrix);

        // 正交投影
        // GL.LoadOrtho();

        // Draw lines


        DrawInnerPoints();

        DrawOutPoints();


        GL.PopMatrix();
    }




    // Update is called once per frame
    void Update()
    {

    }
}
