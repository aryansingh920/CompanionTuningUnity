using UnityEngine;
#if UNITY_XR_MANAGEMENT
using UnityEngine.XR.Management;
#endif
using System.Collections;

public class SceneInitializer : MonoBehaviour
{
    public bool useXR = false;

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
        GameObject companionPrefab = Resources.Load<GameObject>("Character/girl");
        if (companionPrefab == null)
        {
            Debug.LogError("Character/girl prefab not found in Resources.");
            return;
        }

        GameObject companion = Instantiate(companionPrefab);
        companion.name = "Companion";
        companion.transform.position = Vector3.zero;

        if (companion.GetComponent<CharacterController>() == null)
            companion.AddComponent<CharacterController>();

        if (companion.GetComponent<Animator>() == null)
            companion.AddComponent<Animator>();

        if (companion.GetComponent<RuntimeAnimatorSetup>() == null)
            companion.AddComponent<RuntimeAnimatorSetup>();

        if (companion.GetComponent<CompanionController>() == null)
            companion.AddComponent<CompanionController>();
    }

#if UNITY_XR_MANAGEMENT
    IEnumerator StartXR()
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
