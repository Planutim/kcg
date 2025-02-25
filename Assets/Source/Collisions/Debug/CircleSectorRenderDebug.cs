﻿//imports UnityEngine

using KMath;

namespace Collisions
{
    public class CircleSectorRenderDebug : UnityEngine.MonoBehaviour
    {
        public UnityEngine.Material material;
        public UnityEngine.Shader shader;

        private UnityEngine.MeshRenderer meshRenderer;

        [UnityEngine.SerializeField] public UnityEngine.Color color = UnityEngine.Color.white;
        [UnityEngine.SerializeField] public float angle = 60.0f;

        [UnityEngine.SerializeField] public float radius =  4;

        public Vec2f getPos()
        {
            return new Vec2f(meshRenderer.bounds.center.x, meshRenderer.bounds.center.y);
        }

        public void SetPos(Vec2f pos)
        {
            float newTheta = transform.rotation.eulerAngles.z;
            transform.RotateAround(meshRenderer.bounds.center, UnityEngine.Vector3.forward, -newTheta);

            transform.position = new UnityEngine.Vector3(pos.X - meshRenderer.bounds.size.x / 2.0f, 
                pos.Y - meshRenderer.bounds.size.y / 2.0f);

            transform.RotateAround(meshRenderer.bounds.center, UnityEngine.Vector3.forward, newTheta);
        }

        public void Rotate(float theta)
        {
            transform.RotateAround(meshRenderer.bounds.center, UnityEngine.Vector3.forward, theta);
        }

        private void Start()
        {
            var vertices = new UnityEngine.Vector3[4];

            vertices[0] = new UnityEngine.Vector3(0, 0, 0);
            vertices[1] = new UnityEngine.Vector3(1, 0, 0);
            vertices[2] = new UnityEngine.Vector3(0, 1, 0);
            vertices[3] = new UnityEngine.Vector3(1, 1, 0);

            var uvs = new UnityEngine.Vector2[4];

            uvs[0] = new UnityEngine.Vector2(0, 0); //bottom-left
            uvs[1] = new UnityEngine.Vector2(1, 0); //bottom-right
            uvs[2] = new UnityEngine.Vector2(0, 1); //top-left
            uvs[3] = new UnityEngine.Vector2(1, 1); //top-right

            var indices = new int[6];

            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 1;

            indices[3] = 2;
            indices[4] = 3;
            indices[5] = 1;

            shader = UnityEngine.Shader.Find("Unlit/CircleSegmentDraw");
            material = new UnityEngine.Material(shader)
            {
                hideFlags = UnityEngine.HideFlags.HideAndDontSave
            };
           
            var mf = gameObject.AddComponent<UnityEngine.MeshFilter>();
            mf.sharedMesh = new UnityEngine.Mesh
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            mf.sharedMesh.vertices = vertices;
            mf.sharedMesh.triangles = indices;
            mf.sharedMesh.uv = uvs;

            meshRenderer = gameObject.AddComponent<UnityEngine.MeshRenderer>();
            meshRenderer.sharedMaterial = material;
            meshRenderer.sortingOrder = 1000;

            transform.localScale = new UnityEngine.Vector3(2 * radius, 2 * radius, 2 * radius);
        }

        public void Update()
        {
            Vec2f pos = getPos();
            transform.localScale = new UnityEngine.Vector3(2 * radius, 2 * radius, 2 * radius);
            material.SetFloat("_Angle", angle);
            material.SetColor("_Color", color);

            pos = getPos() - pos;
            transform.position -= new UnityEngine.Vector3(pos.X, pos.Y, 0.0f);
        }
    }
}
