using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneMove : MonoBehaviour
{
    //Ư�� ��ư���� ȣ���� �Լ���

    //Opening������ �̵��� �� �ְ� �ϴ� �Լ�
    public void GoOpening()
    {
        SceneManager.LoadScene("Opening");
    }

    //Stage1������ �̵��� �� �ְ� �ϴ� �Լ�
    public void GoS1()
    {
        SceneManager.LoadScene("Stage1");
    }

}
