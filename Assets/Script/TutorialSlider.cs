using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UnityEngine.UI 네임스페이스를 추가하여 UI 요소를 사용 가능하게 함

public class TutorialSlider : MonoBehaviour
{
    public UnityEngine.UI.Image tutorialImage; // 튜토리얼 이미지를 참조하는 변수 선언
    public float delayBeforeSlideIn = 2f; // 시작 후 슬라이드 인 전 대기 시간을 설정하는 변수 선언 및 초기화
    public float slideInDuration = 1f; // 슬라이드 인 애니메이션의 지속 시간을 설정하는 변수 선언 및 초기화
    public float displayDuration = 3f; // 이미지가 화면에 표시되는 시간을 설정하는 변수 선언 및 초기화
    public float slideOutDuration = 1f; // 슬라이드 아웃 애니메이션의 지속 시간을 설정하는 변수 선언 및 초기화

    private Vector2 startPosition; // 이미지의 시작 위치를 저장하는 변수 선언
    private Vector2 endPosition; // 이미지의 끝 위치를 저장하는 변수 선언
    private RectTransform rectTransform; // 튜토리얼 이미지의 RectTransform 컴포넌트를 참조하기 위한 변수 선언

    void Start()
    {
        rectTransform = tutorialImage.GetComponent<RectTransform>(); // 튜토리얼 이미지의 RectTransform 컴포넌트를 가져옴

        // 시작 위치를 화면 밖으로 설정
        startPosition = rectTransform.anchoredPosition; // 현재 위치를 시작 위치로 저장

        // 끝 위치를 화면 안으로 설정 (필요에 따라 수정 가능)
        endPosition = new Vector2(startPosition.x + 500, startPosition.y); // 끝 위치를 설정

        // 시작 시 이미지를 화면 밖으로 이동
        rectTransform.anchoredPosition = startPosition; // 이미지의 시작 위치를 설정

        // 슬라이드 인 애니메이션을 시작하는 코루틴 호출
        StartCoroutine(SlideInOut()); // 슬라이드 인/아웃 애니메이션을 실행하는 코루틴 시작
    }

    private IEnumerator SlideInOut()
    {
        // 슬라이드 인 애니메이션 전 대기 시간
        yield return new WaitForSeconds(delayBeforeSlideIn); // 지정된 시간 동안 대기

        // 슬라이드 인 애니메이션
        yield return StartCoroutine(Slide(startPosition, endPosition, slideInDuration)); // 시작 위치에서 끝 위치로 슬라이드 인 애니메이션 실행

        // 지정된 시간 동안 이미지가 화면에 표시
        yield return new WaitForSeconds(displayDuration); // 이미지가 화면에 표시되는 동안 대기

        // 슬라이드 아웃 애니메이션
        yield return StartCoroutine(Slide(endPosition, startPosition, slideOutDuration)); // 끝 위치에서 시작 위치로 슬라이드 아웃 애니메이션 실행
    }

    private IEnumerator Slide(Vector2 from, Vector2 to, float duration)
    {
        float elapsedTime = 0f; // 경과 시간을 저장하는 변수 초기화

        while (elapsedTime < duration)
        {
            // 경과 시간에 따라 위치를 보간 (lerp)하여 부드럽게 이동
            rectTransform.anchoredPosition = Vector2.Lerp(from, to, elapsedTime / duration); // 시작 위치에서 끝 위치로의 비율에 따라 위치를 보간
            elapsedTime += Time.deltaTime; // 경과 시간을 업데이트
            yield return null; // 다음 프레임까지 대기
        }

        // 애니메이션이 끝난 후 최종 위치를 정확하게 설정
        rectTransform.anchoredPosition = to; // 최종 위치를 설정
    }
}
