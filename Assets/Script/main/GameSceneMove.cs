using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneMove : MonoBehaviour
{
    //특정 버튼에서 호출할 함수들

    //Opening씬으로 이동할 수 있게 하는 함수
    public void GoOpening()
    {
        SceneManager.LoadScene("Opening");
    }

    //Stage1씬으로 이동할 수 있게 하는 함수
    public void GoS1()
    {
        SceneManager.LoadScene("Stage1");
    }

}
