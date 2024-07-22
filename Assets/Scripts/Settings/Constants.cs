using UnityEngine;

public static class Constants
{
    public const float VIEW_CHANGE_TIME = 0.5f;
    public const int VIEW_SPEED = 6;

    public const int MAP_SIZE = 5;
    public const int CUBE_MAXSIZE = 4;
    public const int CUBE_SIMUL_LAYER = 10;
    public const int CUBE_DROP_HEIGHT = 15;
}

public static class SavePath
{
    public const string CUBE_BATCH = "Assets/Resources/CubeBatch/";
}

public static class PlayerKey
{
    public static KeyCode RotateFront = KeyCode.S;
    public static KeyCode RotateRight = KeyCode.D;
    public static KeyCode RotateLeft = KeyCode.A;
    public static KeyCode RotateBack = KeyCode.W;
    public static KeyCode RotateYRight = KeyCode.E;
    public static KeyCode RotateYLEFT = KeyCode.Q;
}

public static class ScoreSystem
{
    public const int SCORE_CUBE_BATCH = 10;
    public const int SCORE_CUBE_MATCH = 300;
    public const int SCORE_CUBE_COMBO = 600;
}

public static class RankSystem
{
    public const int RANK_MAX_PLAYER = 3;
}

public static class SceneName
{
    public const string GAME_SCENE = "GameScene";
    public const string TITLE_SCENE = "TitleScene";
}