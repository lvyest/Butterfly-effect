using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BulletCountUI : MonoBehaviour
{
    public UnityEngine.UI.Text BulletCountText; // UI Text 요소를 연결하기 위한 변수 선언

    private int bulletCount = 0; // 현재 총알 개수를 저장하는 변수 초기화

    // 총알 개수를 증가시키는 함수
    public void IncreaseBulletCount()
    {
        bulletCount++; // 총알 개수를 증가
        UpdateBulletCountUI(); // UI에 총알 개수를 업데이트
    }

    // 총알 개수를 감소시키는 함수
    public void DecreaseBulletCount()
    {
        bulletCount--; // 총알 개수를 감소
        UpdateBulletCountUI(); // UI에 총알 개수를 업데이트
    }

    // UI Text에 현재 총알 개수를 업데이트하는 함수
    private void UpdateBulletCountUI()
    {
        BulletCountText.text = "Bullet Count: " + bulletCount.ToString(); // UI 텍스트를 현재 총알 개수로 설정
    }
}
