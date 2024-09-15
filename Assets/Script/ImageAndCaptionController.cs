using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;

public class ImageAndCaptionController : MonoBehaviour
{
    public UnityEngine.UI.Image displayImage; // 이미지 UI 요소
    public UnityEngine.UI.Text captionText;   // 자막 UI 요소
    public Button nextButton;  // 다음 버튼 UI 요소

    public Sprite[] images;    // 전환할 이미지 배열
    public string[] captions;  // 전환할 자막 배열
    public AudioClip spacebarSound; // 스페이스바를 누를 때 재생할 효과음

    private int currentIndex = 0; // 현재 표시된 이미지와 자막의 인덱스
    private bool isAutoTransition = false; // 자동 전환 상태를 나타내는 플래그
    private const int lastIndex = 24; // 마지막 인덱스
    private const int fadeTextUntilIndex = 5; // 텍스트 페이드 인 효과를 적용할 최대 인덱스

    public float fadeDuration = 1f; // 페이드 인 효과의 지속 시간

    private AudioSource audioSource; // 효과음 재생을 위한 AudioSource 컴포넌트

    void Start()
    {
        nextButton.gameObject.SetActive(false); // 버튼을 비활성화
        audioSource = gameObject.AddComponent<AudioSource>(); // AudioSource 컴포넌트를 게임 오브젝트에 추가
        UpdateDisplay(); // 처음 UI를 업데이트
    }

    void Update()
    {
        // 자동 전환 중에는 스페이스바 입력을 무시
        if (isAutoTransition) return;

        // 스페이스바 입력을 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 효과음 재생
            PlaySound(spacebarSound);

            // 다음 인덱스로 전환
            currentIndex++;

            // 자동 전환 구간에 들어가면 코루틴 시작
            if (currentIndex == 8)
            {
                StartCoroutine(AutoTransition(0.1f)); // 0.1초 간격으로 자동 전환
            }
            else
            {
                UpdateDisplay(); // 이미지와 자막을 업데이트
            }
        }
    }

    void UpdateDisplay()
    {
        // 현재 인덱스에 맞는 이미지와 자막을 설정
        if (currentIndex < images.Length)
        {
            displayImage.sprite = images[currentIndex]; // 이미지 업데이트
        }

        if (currentIndex < captions.Length)
        {
            if (currentIndex <= fadeTextUntilIndex)
            {
                // 인덱스 5까지는 페이드 인 효과 적용
                captionText.text = captions[currentIndex];
                StartCoroutine(FadeInText()); // 텍스트 페이드 인 효과
            }
            else
            {
                // 인덱스 5 이후는 즉시 텍스트 설정
                captionText.text = captions[currentIndex];
                captionText.GetComponent<CanvasGroup>().alpha = 1; // 즉시 텍스트 보이게 설정
            }
        }

        // 마지막 인덱스에 도달하면 버튼 활성화 및 페이드 인
        if (currentIndex == lastIndex)
        {
            nextButton.gameObject.SetActive(true); // 다음 버튼 활성화
            StartCoroutine(FadeInButton()); // 버튼 페이드 인 효과
        }
    }

    IEnumerator AutoTransition(float interval)
    {
        isAutoTransition = true; // 자동 전환 상태로 설정

        for (int i = currentIndex; i <= 17; i++)
        {
            UpdateDisplay(); // 현재 디스플레이 업데이트
            yield return new WaitForSeconds(interval); // 지정된 간격 동안 대기
            currentIndex++; // 다음 인덱스로 이동
        }

        isAutoTransition = false; // 자동 전환 종료
        UpdateDisplay(); // 마지막 이미지를 업데이트
    }

    IEnumerator FadeInText()
    {
        // 텍스트의 CanvasGroup을 가져오거나 추가
        CanvasGroup canvasGroup = captionText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = captionText.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0; // 텍스트를 투명하게 설정
        float elapsedTime = 0; // 경과 시간 초기화

        // 지정된 지속 시간 동안 알파 값을 증가시켜 페이드 인 효과 적용
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // 알파 값 증가
            yield return null; // 다음 프레임까지 대기
        }
    }

    IEnumerator FadeInButton()
    {
        // 버튼의 CanvasGroup을 가져오거나 추가
        CanvasGroup canvasGroup = nextButton.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = nextButton.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0; // 버튼을 투명하게 설정
        float elapsedTime = 0; // 경과 시간 초기화

        // 지정된 지속 시간 동안 알파 값을 증가시켜 페이드 인 효과 적용
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // 알파 값 증가
            yield return null; // 다음 프레임까지 대기
        }
    }

    // 효과음을 재생하는 메서드
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip); // 지정된 효과음을 재생
        }
    }
}
