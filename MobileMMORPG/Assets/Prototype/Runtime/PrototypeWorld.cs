using System.Collections.Generic;
using UnityEngine;

namespace MobileMMORPG.Prototype
{
    public sealed class PrototypeWorld : MonoBehaviour
    {
        private readonly List<Material> materials = new List<Material>();

        private void Start()
        {
            Material ground = MakeMaterial(new Color(0.28f, 0.43f, 0.32f));
            Material stone = MakeMaterial(new Color(0.48f, 0.52f, 0.58f));
            Material playerMaterial = MakeMaterial(new Color(0.15f, 0.65f, 0.95f));
            Material markerMaterial = MakeMaterial(new Color(1f, 0.7f, 0.15f));

            Box("Ground", new Vector3(0f, -0.5f, 0f), new Vector3(40f, 1f, 40f), ground);
            Box("Obstacle A", new Vector3(0f, 0.75f, 5f), new Vector3(4f, 1.5f, 2f), stone);
            Box("Obstacle B", new Vector3(-6f, 1f, 0f), new Vector3(2f, 2f, 5f), stone);
            Box("Obstacle C", new Vector3(6f, 0.5f, -3f), new Vector3(3f, 1f, 3f), stone);
            Box("North boundary", new Vector3(0f, 0.5f, 20f), new Vector3(41f, 1f, 1f), stone);
            Box("South boundary", new Vector3(0f, 0.5f, -20f), new Vector3(41f, 1f, 1f), stone);
            Box("East boundary", new Vector3(20f, 0.5f, 0f), new Vector3(1f, 1f, 40f), stone);
            Box("West boundary", new Vector3(-20f, 0.5f, 0f), new Vector3(1f, 1f, 40f), stone);

            GameObject player = new GameObject("Player");
            player.transform.SetParent(transform, false);
            player.transform.position = new Vector3(0f, 0.1f, -6f);
            CharacterController body = player.AddComponent<CharacterController>();
            body.height = 2f;
            body.radius = 0.45f;
            body.center = Vector3.up;
            body.stepOffset = 0.25f;
            body.slopeLimit = 45f;
            body.skinWidth = 0.03f;
            GameObject visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.name = "Capsule visual";
            visual.transform.SetParent(player.transform, false);
            visual.transform.localPosition = Vector3.up;
            visual.GetComponent<Collider>().enabled = false;
            visual.GetComponent<Renderer>().sharedMaterial = playerMaterial;
            // 캐릭터가 바라보는 방향을 알 수 있도록 앞에 작은 표시를 붙인다.
            GameObject marker = Box("Forward marker", Vector3.zero, new Vector3(0.2f, 0.2f, 0.2f), markerMaterial);
            marker.GetComponent<Collider>().enabled = false;
            marker.transform.SetParent(player.transform, false);
            marker.transform.localPosition = new Vector3(0f, 1.3f, 0.48f);
            PrototypeMotor motor = player.AddComponent<PrototypeMotor>();

            GameObject cameraObject = new GameObject("Main Camera");
            cameraObject.transform.SetParent(transform, false);
            cameraObject.tag = "MainCamera";
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.nearClipPlane = 0.1f;
            camera.farClipPlane = 150f;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0.45f, 0.65f, 0.8f);
            cameraObject.AddComponent<AudioListener>();
            PrototypeCamera orbit = cameraObject.AddComponent<PrototypeCamera>();
            orbit.SetTarget(player.transform);
            motor.SetView(cameraObject.transform);

            GameObject lightObject = new GameObject("Sun");
            lightObject.transform.SetParent(transform, false);
            lightObject.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
            Light sun = lightObject.AddComponent<Light>();
            sun.type = LightType.Directional;
            sun.intensity = 1.5f;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(0.55f, 0.55f, 0.55f);
            Physics.SyncTransforms();
        }

        private GameObject Box(string label, Vector3 position, Vector3 size, Material material)
        {
            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            box.name = label;
            box.transform.SetParent(transform, false);
            box.transform.position = position;
            box.transform.localScale = size;
            box.GetComponent<Renderer>().sharedMaterial = material;
            return box;
        }

        private Material MakeMaterial(Color color)
        {
            Material material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.SetColor("_BaseColor", color);
            materials.Add(material);
            return material;
        }

        private void OnGUI()
        {
            GUI.Box(new Rect(12, 12, 490, 62), "Third-person prototype\nWASD: Move | Hold Right Mouse: Look | Esc: Release cursor");
        }

        private void OnDestroy()
        {
            foreach (Material material in materials)
                if (material != null) Destroy(material);
        }
    }
}
