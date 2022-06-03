using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;


public sealed class Creator : EditorWindow
{

    #region Fields

    private const int MIN_SCALE_LEFT = 50;
    private const int MIN_SCALE_RIGHT = 100;
    private const int MAX_SCALE_LEFT = 100;
    private const int MAX_SCALE_RIGHT = 150;

    private GameObject _gameObject;
    private Transform _parentTransform;
    private bool _isCreating;
    private bool _randomRotationY;
    private int _minScale = 100;
    private int _maxScale = 100;

    #endregion


    #region UnityMethods

    private void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
    }

    private void OnGUI()
    {
        EditorGUILayout.Space(20f);
        _gameObject = (GameObject)EditorGUILayout.ObjectField("GameObject", _gameObject, typeof(GameObject), true);
        EditorGUILayout.Space(10f);
        _parentTransform = (Transform)EditorGUILayout.ObjectField("Parent", _parentTransform, typeof(Transform), true);
        EditorGUILayout.Space(10f);
        _randomRotationY = EditorGUILayout.Toggle("Random rotation Y", _randomRotationY);
        EditorGUILayout.Space(10f);
        _minScale = EditorGUILayout.IntSlider("Min scale, %", _minScale, MIN_SCALE_LEFT, MIN_SCALE_RIGHT);
        EditorGUILayout.Space(10f);
        _maxScale = EditorGUILayout.IntSlider("Max scale, %", _maxScale, MAX_SCALE_LEFT, MAX_SCALE_RIGHT);
        EditorGUILayout.Space(20f);
        _isCreating = EditorGUILayout.Toggle("Enable creation (RMB)", _isCreating);
    }

    #endregion


    #region Window / menu

    [MenuItem("Vasco Tools/Creator")]
    private static void MenuOption()
    {
        GetWindow(typeof(Creator), false, "Creator");
    }

    #endregion


    #region Methods


    void SceneGUI(SceneView sceneView)
    {
        if (Event.current.type == EventType.MouseDown && Event.current.button == 1 && _isCreating && _gameObject != default)
        {
            OnMouseDown(sceneView);
        }
    }

    private void OnMouseDown(SceneView scene)
    {
        Ray ray = scene.camera.ScreenPointToRay(CalculatePosition(scene));
        InstantiateObject(ray);
    }

    private void InstantiateObject(Ray ray)
    {
        RaycastHit hit;
        Vector3 scale = (_minScale + (_maxScale - _minScale) * Random.value) / 100.0f * Vector3.one;

        if (Physics.Raycast(ray, out hit))
        {
            Instantiate(
                _gameObject,
                hit.point,
                Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f) * (_randomRotationY ? 1 : 0), 0.0f),
                _parentTransform)
                .transform.localScale = scale;

            Debug.Log($"Instantiated {_gameObject.name} at {hit.point}");
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }


    }

    private Vector3 CalculatePosition(SceneView scene)
    {
        Vector3 mousePos = Event.current.mousePosition;
        float ppp = EditorGUIUtility.pixelsPerPoint;
        mousePos.y = scene.camera.pixelHeight - mousePos.y * ppp;
        mousePos.x *= ppp;

        return mousePos;
    }

    #endregion

}
