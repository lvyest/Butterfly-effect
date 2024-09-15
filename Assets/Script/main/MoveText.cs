using UnityEngine;
using UnityEngine.UI;

public class MoveText: MonoBehaviour
{
    public RectTransform element; // ������ UI ����� RectTransform
    public float speed = 50f; // �̵� �ӵ�
    public float upperLimit = 300f; // ���� �Ѱ�
    public float lowerLimit = -300f; // �Ʒ��� �Ѱ�

    private Vector3 direction = Vector3.up; // �ʱ� ������ ����

    void Update()
    {
        // UI ��Ҹ� ���� �������� �̵�
        element.localPosition += direction * speed * Time.deltaTime;

        // ���� �Ѱ迡 �����ϸ� ������ �Ʒ��� ����
        if (element.localPosition.y >= upperLimit)
        {
            direction = Vector3.down;
        }
        // �Ʒ��� �Ѱ迡 �����ϸ� ������ ���� ����
        else if (element.localPosition.y <= lowerLimit)
        {
            direction = Vector3.up;
        }
    }
}
