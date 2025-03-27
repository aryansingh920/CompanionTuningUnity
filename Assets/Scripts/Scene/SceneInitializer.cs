using UnityEngine;
#if UNITY_XR_MANAGEMENT
using UnityEngine.XR.Management;
#endif

public class SceneInitializer : MonoBehaviour
{
    public bool useXR = false; // Toggle this to enable XR mode

    void Start()
    {
#if UNITY_XR_MANAGEMENT
    if (useXR)
        StartCoroutine(StartXR());
    else
        SetupCamera();
#else
        SetupCamera();
#endif

        new GameObject("InputManager", typeof(InputManager));
        SetupLight();
        SpawnCompanion();
    }

    void SetupCamera()
    {
        GameObject cameraObj = new GameObject("Main Camera");
        Camera camera = cameraObj.AddComponent<Camera>();
        cameraObj.tag = "MainCamera";
        cameraObj.transform.position = new Vector3(0, 2, -10);
        cameraObj.transform.LookAt(Vector3.zero);
    }

    void SetupLight()
    {
        GameObject lightObj = new GameObject("Directional Light");
        Light lightComp = lightObj.AddComponent<Light>();
        lightComp.type = LightType.Directional;
        lightObj.transform.rotation = Quaternion.Euler(50f, -30f, 0);
    }

    void SpawnCompanion()
    {
        GameObject companion = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        companion.name = "Companion";
        companion.transform.position = Vector3.zero;

        // Remove collider from primitive (optional, to avoid physics issues)
        Destroy(companion.GetComponent<Collider>());

        // Add CharacterController for smooth movement
        companion.AddComponent<CharacterController>();

        // Add animator if needed
        // Animator animator = companion.AddComponent<Animator>();
        // animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/CompanionController");

        // Add your movement script
        companion.AddComponent<CompanionController>();
    }

#if UNITY_XR_MANAGEMENT
    System.Collections.IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Failed to initialize XR Loader.");
            yield break;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        Debug.Log("XR Started.");
    }
#endif
}
