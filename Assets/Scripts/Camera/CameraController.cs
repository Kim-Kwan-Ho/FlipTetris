using UnityEngine;

public class CameraController : BaseBehaviour
{
    [Header("Camera Property")]
    [SerializeField] private Transform _centerTrs;
    [SerializeField] private CameraPosition[] _positions;
    private bool _changeable;




    private ESceneView _sceneView;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Initialize()
    {
        base.Initialize();
        _sceneView = ESceneView.Down;
    }


    private void Update()
    {
        GetKeyInput();
    }


    private void GetKeyInput()
    {

    }

    private void CallViewChangedEvent()
    {
        ViewManager.ViewChangedEvent?.Invoke(_sceneView);
    }


#if UNITY_EDITOR
    protected override void OnBindField()
    {
        base.OnBindField();
        _centerTrs = GameObject.FindGameObjectWithTag("Center").transform;
        _positions = GameObject.FindObjectsOfType<CameraPosition>();
    }
#endif

}