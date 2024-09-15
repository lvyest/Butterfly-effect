using UnityEngine; 
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 
using System.Collections; 

public class GaugeControl : MonoBehaviour 
{
    public Slider gaugeSlider; // UI 슬라이더
    public GameObject[] flames; // 불 오브젝트 배열
    public float increaseRate = 10f; // 게이지 증가 속도
    public float decreaseAmount = 1f; // 스페이스바 누를 때 감소량
    public Animator butterflyAnimator; // 나비의 애니메이터
    public float flameShakeAmount = 0.1f; // 불의 좌우 휘청거림 양
    public float flameShakeSpeed = 5f; // 불의 휘청거림 속도
    public GameObject gameOverImage; // 게임 오버 이미지 오브젝트

    private float gaugeValue = 500f; // 게이지 초기값
    private Vector3[] flameOriginalPositions; // 불의 원래 위치 배열
    private SpriteRenderer[] flameRenderers; // 불의 스프라이트 렌더러 배열
    public float fadeDuration = 2f; // 페이드 아웃 지속 시간
    private bool isGameOver = false; // 게임 오버 상태 플래그

    void Start() 
    {
        gaugeSlider.value = gaugeValue; // 초기 게이지 값 설정

        flameOriginalPositions = new Vector3[flames.Length]; // 불 오브젝트의 원래 위치 배열 초기화
        flameRenderers = new SpriteRenderer[flames.Length]; // 불 오브젝트의 스프라이트 렌더러 배열 초기화

        for (int i = 0; i < flames.Length; i++) // 불 오브젝트 각각에 대해 반복
        {
            flameOriginalPositions[i] = flames[i].transform.localPosition; // 각 불의 원래 위치 저장
            flameRenderers[i] = flames[i].GetComponent<SpriteRenderer>(); // 각 불의 스프라이트 렌더러 저장
        }

        gameOverImage.SetActive(false); // 게임 오버 이미지 비활성화
    }

    void Update() { 
        if (isGameOver) // 게임 오버 상태인지 확인
        {
            if (Input.GetMouseButtonDown(0)) // 마우스 버튼 클릭 확인
            {
                LoadNextStage(); // 다음 스테이지 로드
            }
            return; // 게임 오버 상태에서는 더 이상 업데이트하지 않음
        }

        if (gaugeValue < 500f) // 게이지가 최대값보다 작은 경우
        {
            gaugeValue += increaseRate * Time.deltaTime; // 게이지 증가
            gaugeSlider.value = gaugeValue; // 슬라이더 업데이트
        }

        if (gaugeValue <= 5f && !isGameOver) // 게이지가 5 이하이고 게임 오버 상태가 아닌 경우
        {
            StartCoroutine(FadeOutFlames()); // 불 오브젝트 페이드 아웃 코루틴 시작
        }

        ShakeFlames(); // 불 휘청거림 효과 적용

        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바 입력 확인
        {
            OnSpacebarPress(); // 스페이스바 눌림 처리
        }
    }

    void OnSpacebarPress() // 스페이스바 눌림 처리 메서드
    {
        gaugeValue -= decreaseAmount; // 게이지 감소

        if (gaugeValue < 0f) // 게이지 값이 0 이하가 되지 않도록 제한
        {
            gaugeValue = 0f; // 최소값으로 설정
        }

        gaugeSlider.value = gaugeValue; // 슬라이더 업데이트

        butterflyAnimator.SetTrigger("flap"); // 나비 날갯짓 애니메이션 트리거
    }

    void ShakeFlames() // 불 오브젝트 휘청거림 처리 메서드
    {
        for (int i = 0; i < flames.Length; i++) // 각 불 오브젝트에 대해 반복
        {
            float shakeOffset = Mathf.Sin(Time.time * flameShakeSpeed) * flameShakeAmount; // 좌우로 흔들리게 할 오프셋 계산
            flames[i].transform.localPosition = new Vector3(flameOriginalPositions[i].x + shakeOffset, flameOriginalPositions[i].y, flameOriginalPositions[i].z); // 불의 새로운 위치 설정
        }
    }

    IEnumerator FadeOutFlames() // 불 오브젝트를 페이드 아웃하는 코루틴 메서드
    {
        float elapsedTime = 0f; // 경과 시간 초기화
        Color[] originalColors = new Color[flameRenderers.Length]; // 각 불의 원래 색상 배열 초기화

        for (int i = 0; i < flameRenderers.Length; i++) // 각 불 오브젝트에 대해 반복
        {
            originalColors[i] = flameRenderers[i].color; // 각 불의 원래 색상 저장
        }

        while (elapsedTime < fadeDuration) // 페이드 아웃 지속 시간 동안 반복
        {
            elapsedTime += Time.deltaTime; // 경과 시간 증가
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // 알파 값 계산

            for (int i = 0; i < flameRenderers.Length; i++) // 각 불 오브젝트에 대해 반복
            {
                flameRenderers[i].color = new Color(originalColors[i].r, originalColors[i].g, originalColors[i].b, alpha); // 각 불의 투명도 조절
            }

            yield return null; // 다음 프레임까지 대기
        }

        foreach (GameObject flame in flames) // 모든 불 오브젝트에 대해 반복
        {
            flame.SetActive(false); // 불 오브젝트 비활성화
        }

        GameOver(); // 게임 오버 처리 호출
    }

    void GameOver() // 게임 오버 처리 메서드
    {
        isGameOver = true; // 게임 오버 상태 설정
        gameOverImage.SetActive(true); // 게임 오버 이미지 활성화
    }

    void LoadNextStage() // 다음 스테이지로 씬 전환하는 메서드
    {
        SceneManager.LoadScene("Stage2"); // "Stage2" 씬 로드
    }
}
