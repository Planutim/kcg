//imports UnityEngine

using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

[UnityEngine.ExecuteInEditMode]
[UnityEngine.RequireComponent(typeof(UnityEngine.Camera))]
public class PixelPerfectCameraTestTool : UnityEngine.MonoBehaviour
{
    //Pixels per unit stores
    public static int PIXELS_PER_UNIT = 100;

    // Width and Height
    public enum Dimension { Width, Height };
    // Constraint Set for Dimension
    public enum ConstraintType { None, Horizontal, Vertical };

    // Inputs
    public bool maxCameraHalfWidthEnabled = false;
    public bool maxCameraHalfHeightEnabled = false;
    public float maxCameraHalfWidth = 3;
    public float maxCameraHalfHeight = 2.0f;
    public Dimension targetDimension = Dimension.Height;
    public float targetCameraHalfWidth = 2.0f;
    public float targetCameraHalfHeight = 1.5f;
    public bool pixelPerfect = false;
    public float assetsPixelsPerUnit = PIXELS_PER_UNIT;

    // Outputs
    [NonSerialized]
    public UnityEngine.Vector2 cameraSize;
    [NonSerialized]
    public ConstraintType contraintUsed;
    [NonSerialized]
    public float cameraPixelsPerUnit;
    [NonSerialized]
    public float ratio;
    [NonSerialized]
    public UnityEngine.Vector2 nativeAssetResolution;
    [NonSerialized]
    public float fovCoverage;
    [NonSerialized]
    public bool isInitialized;

    // Internals
    UnityEngine.Resolution res;
    UnityEngine.Camera cam;

    float calculatePixelPerfectCameraSize(bool pixelPerfect, UnityEngine.Resolution res, float assetsPixelsPerUnit, float maxCameraHalfWidth, float maxCameraHalfHeight
        , float targetHalfWidth, float targetHalfHeight, Dimension targetDimension)
    {
        float maxHorizontalFOV = 2f * maxCameraHalfWidth;
        float maxVerticalFOV = 2f * maxCameraHalfHeight;
        float targetWidth = 2f * targetHalfWidth;
        float targetHeight = 2f * targetHalfHeight;
        float AR = (float)res.width / res.height;

        // How many screen pixels will an asset pixel render to?
        // or how many times will the asset dimensions be multiplied?
        float ratioTarget;

        if (targetDimension == Dimension.Width)
        {
            float assetsWidth = assetsPixelsPerUnit * targetWidth;
            ratioTarget = (float)res.width / assetsWidth;
        }
        else
        {
            float assetsHeight = assetsPixelsPerUnit * targetHeight;
            ratioTarget = (float)(float)res.height / assetsHeight;
        }
        float ratioTargetOriginal = ratioTarget;
        if (pixelPerfect)
        {
            float ratioSnapped = UnityEngine.Mathf.Ceil(ratioTarget);
            float ratioSnappedPrevious = ratioSnapped - 1;
            // choose the ratio whose fov (or native asset resolution) is nearest to the ratioTarget's fov
            ratioTarget = (1 / ratioTarget - 1 / ratioSnapped < 1 / ratioSnappedPrevious - 1 / ratioTarget) ? ratioSnapped : ratioSnappedPrevious;
            if (ratioSnapped <= 1)
            {
                ratioTarget = 1;
            }
        }

        float ratioHorizontal = 0;
        float ratioVertical = 0;
        if (maxHorizontalFOV > 0f)
        {
            float assetsWidth = assetsPixelsPerUnit * maxHorizontalFOV;
            ratioHorizontal = (float)res.width / assetsWidth;
        }
        if (maxVerticalFOV > 0f)
        {
            float assetsHeight = assetsPixelsPerUnit * maxVerticalFOV;
            ratioVertical = (float)res.height / assetsHeight;
        }
        float ratioMin = UnityEngine.Mathf.Max(ratioHorizontal, ratioVertical);
        if (pixelPerfect)
        {
            ratioMin = UnityEngine.Mathf.Ceil(ratioMin);
        }
        float ratioUsed = UnityEngine.Mathf.Max(ratioMin, ratioTarget);

        float horizontalFOV = res.width / (assetsPixelsPerUnit * ratioUsed);
        float verticalFOV = horizontalFOV / AR;

        // ------ GUI Calculations  -----
        cameraSize = new UnityEngine.Vector2(horizontalFOV / 2, verticalFOV / 2);
        bool unconstrained = ratioTarget >= UnityEngine.Mathf.Max(ratioHorizontal, ratioVertical) && ratioTargetOriginal >= UnityEngine.Mathf.Max(ratioHorizontal, ratioVertical);
        contraintUsed = (unconstrained) ? ConstraintType.None : (ratioHorizontal > ratioVertical) ? ConstraintType.Horizontal : ConstraintType.Vertical;
        cameraPixelsPerUnit = (float)res.width / horizontalFOV;
        ratio = ratioUsed;
        nativeAssetResolution = new UnityEngine.Vector2(horizontalFOV * assetsPixelsPerUnit, verticalFOV * assetsPixelsPerUnit);
        fovCoverage = ratioTargetOriginal / ratioUsed;
        isInitialized = true;
        // ------ GUI Calculations End  -----

        return verticalFOV / 2;
    }

    public void adjustCameraFOV()
    {
        if (cam == null)
        {
            cam = GetComponent<UnityEngine.Camera>();
        }
        res = new UnityEngine.Resolution();
        res.width = cam.pixelWidth;
        res.height = cam.pixelHeight;
        res.refreshRate = UnityEngine.Screen.currentResolution.refreshRate;

        if (res.width == 0 || res.height == 0)
        {
            return;
        }

        float maxCameraHalfWidthReq = (maxCameraHalfWidthEnabled) ? maxCameraHalfWidth : -1;
        float maxCameraHalfHeightReq = (maxCameraHalfHeightEnabled) ? maxCameraHalfHeight : -1;
        float cameraSize = calculatePixelPerfectCameraSize(pixelPerfect, res, assetsPixelsPerUnit, maxCameraHalfWidthReq, maxCameraHalfHeightReq, targetCameraHalfWidth, targetCameraHalfHeight, targetDimension);

        cam.orthographicSize = cameraSize;
    }

    // Doc : https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
    void OnEnable()
    {
        adjustCameraFOV();
    }

    // Doc : https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnValidate.html
    void OnValidate()
    {
        maxCameraHalfWidth = Math.Max(maxCameraHalfWidth, 0.01f);
        maxCameraHalfHeight = Math.Max(maxCameraHalfHeight, 0.01f);
        targetCameraHalfWidth = Math.Max(targetCameraHalfWidth, 0.01f);
        targetCameraHalfHeight = Math.Max(targetCameraHalfHeight, 0.01f);
        adjustCameraFOV();
    }

    //#if UNITY_EDITOR
    // Doc : https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
    void Update()
    {
        if (res.width != cam.pixelWidth || res.height != cam.pixelHeight)
        {
            adjustCameraFOV();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PixelPerfectCameraTestTool))]
[CanEditMultipleObjects]
public class PixelPerfectCameraTestToolEditor : Editor
{
    SerializedProperty maxCameraHalfWidthEnabled;
    SerializedProperty maxCameraHalfHeightEnabled;
    SerializedProperty maxCameraHalfWidth;
    SerializedProperty maxCameraHalfHeight;
    SerializedProperty targetDimension;
    SerializedProperty targetCameraHalfWidth;
    SerializedProperty targetCameraHalfHeight;
    SerializedProperty pixelPerfect;
    SerializedProperty assetsPixelsPerUnit;

    void OnEnable()
    {
        maxCameraHalfWidthEnabled = serializedObject.FindProperty("maxCameraHalfWidthEnabled");
        maxCameraHalfHeightEnabled = serializedObject.FindProperty("maxCameraHalfHeightEnabled");
        maxCameraHalfWidth = serializedObject.FindProperty("maxCameraHalfWidth");
        maxCameraHalfHeight = serializedObject.FindProperty("maxCameraHalfHeight");
        targetDimension = serializedObject.FindProperty("targetDimension");
        targetCameraHalfWidth = serializedObject.FindProperty("targetCameraHalfWidth");
        targetCameraHalfHeight = serializedObject.FindProperty("targetCameraHalfHeight");
        pixelPerfect = serializedObject.FindProperty("pixelPerfect");
        assetsPixelsPerUnit = serializedObject.FindProperty("assetsPixelsPerUnit");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Targeted Size
        PixelPerfectCameraTestTool.Dimension dimensionType = (PixelPerfectCameraTestTool.Dimension)Enum.GetValues(typeof(PixelPerfectCameraTestTool.Dimension)).GetValue(targetDimension.enumValueIndex);
        targetDimension.enumValueIndex = (int)(PixelPerfectCameraTestTool.Dimension)EditorGUILayout.EnumPopup("Target size", dimensionType);
        if (targetDimension.enumValueIndex == (int)PixelPerfectCameraTestTool.Dimension.Width)
        {
            EditorGUILayout.PropertyField(targetCameraHalfWidth, new UnityEngine.GUIContent("Width", "The targetted half width of the camera."));
        }
        else
        {
            EditorGUILayout.PropertyField(targetCameraHalfHeight, new UnityEngine.GUIContent("Height", "The targetted half height of the camera."));
        }
        EditorGUILayout.BeginHorizontal();
        maxCameraHalfWidthEnabled.boolValue = EditorGUILayout.Toggle(maxCameraHalfWidthEnabled.boolValue, UnityEngine.GUILayout.Width(12));
        EditorGUI.BeginDisabledGroup(!maxCameraHalfWidthEnabled.boolValue);
        EditorGUILayout.PropertyField(maxCameraHalfWidth, new UnityEngine.GUIContent("Max Width", "The maximum allowed half width of the camera."));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        maxCameraHalfHeightEnabled.boolValue = EditorGUILayout.Toggle(maxCameraHalfHeightEnabled.boolValue, UnityEngine.GUILayout.Width(12));
        EditorGUI.BeginDisabledGroup(!maxCameraHalfHeightEnabled.boolValue);
        EditorGUILayout.PropertyField(maxCameraHalfHeight, new UnityEngine.GUIContent("Max Height", "The maximum allowed half height of the camera."));
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        // Pixels Per Unit
        EditorGUILayout.PropertyField(assetsPixelsPerUnit);

        // Pixel Perfect toggle
        pixelPerfect.boolValue = EditorGUILayout.Toggle(new UnityEngine.GUIContent("Pixel Perfect",
            "Makes the camera's pixels per unit to be a multiple of the assets' pixels per unit."), pixelPerfect.boolValue);

        serializedObject.ApplyModifiedProperties();

        // Show results
        if (!((PixelPerfectCameraTestTool)target).isInitialized)
            return;
        UnityEngine.GUILayout.BeginVertical();
        UnityEngine.GUILayout.Space(5);
        UnityEngine.GUILayout.EndVertical();
    }

    private string makeBold(string str)
    {
        return "<b>" + str + "</b>";
    }


}
#endif
