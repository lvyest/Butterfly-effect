using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 아이템의 종류를 나타내는 열거형 선언
    public enum ItemType
    {
        Shrink,        // 플레이어 크기 축소 아이템
        IncreaseSpeed // 이동 속도 향상 아이템
    }

    public ItemType itemType; // 아이템의 종류를 저장하는 변수 선언

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 충돌한 객체가 플레이어인 경우를 확인
        {
            gameObject.SetActive(false); // 충돌한 아이템을 비활성화

            // 충돌한 아이템의 종류에 따라 효과를 적용
            switch (itemType)
            {
                case ItemType.Shrink:
                    other.GetComponent<PlayerMovev1>().ShrinkPlayer(); // 플레이어의 크기를 줄이는 메서드 호출
                    break;
                case ItemType.IncreaseSpeed:
                    other.GetComponent<PlayerMovev1>().IncreaseSpeed(5); // 플레이어의 속도를 증가시키는 메서드 호출
                    break;
                default:
                    break;
            }
        }
    }
}
