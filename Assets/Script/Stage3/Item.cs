using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Item : MonoBehaviour
{
    // �������� ������ ��Ÿ���� ������ ����
    public enum ItemType
    {
        Shrink,        // �÷��̾� ũ�� ��� ������
        IncreaseSpeed // �̵� �ӵ� ��� ������
    }

    public ItemType itemType; // �������� ������ �����ϴ� ���� ����

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // �浹�� ��ü�� �÷��̾��� ��츦 Ȯ��
        {
            gameObject.SetActive(false); // �浹�� �������� ��Ȱ��ȭ

            // �浹�� �������� ������ ���� ȿ���� ����
            switch (itemType)
            {
                case ItemType.Shrink:
                    other.GetComponent<PlayerMovev1>().ShrinkPlayer(); // �÷��̾��� ũ�⸦ ���̴� �޼��� ȣ��
                    break;
                case ItemType.IncreaseSpeed:
                    other.GetComponent<PlayerMovev1>().IncreaseSpeed(5); // �÷��̾��� �ӵ��� ������Ű�� �޼��� ȣ��
                    break;
                default:
                    break;
            }
        }
    }
}
