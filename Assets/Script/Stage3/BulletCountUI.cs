using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BulletCountUI : MonoBehaviour
{
    public UnityEngine.UI.Text BulletCountText; // UI Text ��Ҹ� �����ϱ� ���� ���� ����

    private int bulletCount = 0; // ���� �Ѿ� ������ �����ϴ� ���� �ʱ�ȭ

    // �Ѿ� ������ ������Ű�� �Լ�
    public void IncreaseBulletCount()
    {
        bulletCount++; // �Ѿ� ������ ����
        UpdateBulletCountUI(); // UI�� �Ѿ� ������ ������Ʈ
    }

    // �Ѿ� ������ ���ҽ�Ű�� �Լ�
    public void DecreaseBulletCount()
    {
        bulletCount--; // �Ѿ� ������ ����
        UpdateBulletCountUI(); // UI�� �Ѿ� ������ ������Ʈ
    }

    // UI Text�� ���� �Ѿ� ������ ������Ʈ�ϴ� �Լ�
    private void UpdateBulletCountUI()
    {
        BulletCountText.text = "Bullet Count: " + bulletCount.ToString(); // UI �ؽ�Ʈ�� ���� �Ѿ� ������ ����
    }
}
