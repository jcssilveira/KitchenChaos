using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene
    }

    public static Scene targetScene;



    public static void Load(Scene targetScene) {
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
        Loader.targetScene=targetScene;
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
