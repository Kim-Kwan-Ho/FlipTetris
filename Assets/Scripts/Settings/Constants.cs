using UnityEngine;

public static class Constants
{
    public const float VIEW_CHANGE_TIME = 0.5f;

    public const int MAP_SIZE = 5;
    public const int CUBE_MAXSIZE = 4;
    public const int CUBE_SIMUL_LAYER = 10;
    public const int CUBE_DROP_HEIGHT = 12;
}

public static class SavePath
{
    public const string CUBE_BATCH = "Assets/Resources/CubeBatch/";
}

public static class PlayerKey
{
    public static KeyCode RotateFront = KeyCode.W;
    public static KeyCode RotateRight = KeyCode.D;
    public static KeyCode RotateLeft = KeyCode.A;
    public static KeyCode RotateBack = KeyCode.S;
    public static KeyCode RotateCubeX = KeyCode.Q;
    public static KeyCode RotateCubeY = KeyCode.E;
}