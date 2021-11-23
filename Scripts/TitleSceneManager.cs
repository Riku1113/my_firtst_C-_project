using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ボタンを押したらゲームをスタートさせたい
public class TitleSceneManager : MonoBehaviour
{
    public void OnStartButton()
    {
        //Battle Sceneに飛ぶ
        SceneManager.LoadScene("Battle");
    }
}
