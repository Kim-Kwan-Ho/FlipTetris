using System;
using UnityEngine;
using System.Linq;
public class ViewManager : BaseBehaviour
{
    public static Action<ESceneView> ViewChangedEvent;


    [Header("View Changed")]
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



}





public interface IViewChangeable
{
    public void OnViewChanged(ESceneView viewDirection);

}
public enum ESceneView
{
    Down,
    Left,
    Right,
    Front,
    Top,
    Back,
}

public enum ERotateDirection
{
    Forward,
    Right,
    Left,
    Back
}