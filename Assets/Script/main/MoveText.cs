using UnityEngine;
using UnityEngine.UI;

public class MoveText: MonoBehaviour
{
    public RectTransform element; // 움직일 UI 요소의 RectTransform
    public float speed = 50f; // 이동 속도
    public float upperLimit = 300f; // 위쪽 한계
    public float lowerLimit = -300f; // 아래쪽 한계

    private Vector3 direction = Vector3.up; // 초기 방향은 위로

    void Update()
    {
        // UI 요소를 현재 방향으로 이동
        element.localPosition += direction * speed * Time.deltaTime;

        // 위쪽 한계에 도달하면 방향을 아래로 변경
        if (element.localPosition.y >= upperLimit)
        {
            direction = Vector3.down;
        }
        // 아래쪽 한계에 도달하면 방향을 위로 변경
        else if (element.localPosition.y <= lowerLimit)
        {
            direction = Vector3.up;
        }
    }
}
