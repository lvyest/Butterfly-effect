using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageAndCaptionController2 : MonoBehaviour
{
    public UnityEngine.UI.Image displayImage; // 이미지 UI 요소
    public UnityEngine.UI.Text captionText;   // 자막 UI 요소

    public Sprite[] images;    // 전환할 이미지 배열
    public string[] captions;  // 전환할 자막 배열
    public AudioClip spacebarSound; // 스페이스바를 누를 때 재생할 효과음

    private int currentIndex = 0; // 현재 표시된 이미지와 자막의 인덱스
    private bool isAutoTransition = false; // 자동 전환 상태를 나타내는 플래그

    private AudioSource audioSource; // 효과음 재생을 위한 AudioSource 컴포넌트

    void Start()
    {
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
            if (currentIndex == 3)
            {
                StartCoroutine(AutoTransition(0.3f, 20)); // 0.3초 간격으로 자동 전환
            }
            else if (currentIndex == 23)
            {
                StartCoroutine(AutoTransition(0.3f, 28)); // 0.3초 간격으로 자동 전환
            }
            else if (currentIndex == 35)
            {
                StartCoroutine(AutoTransition(0.3f, 40)); // 0.3초 간격으로 자동 전환
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
            // 텍스트를 즉시 설정
            captionText.text = captions[currentIndex];
        }
    }

    IEnumerator AutoTransition(float interval, float end)
    {
        isAutoTransition = true; // 자동 전환 상태로 설정

        for (int i = currentIndex; i <= end; i++)
        {
            UpdateDisplay(); // 현재 디스플레이 업데이트
            yield return new WaitForSeconds(interval); // 지정된 간격 동안 대기
            currentIndex++; // 다음 인덱스로 이동
        }

        isAutoTransition = false; // 자동 전환 종료
        UpdateDisplay(); // 마지막 이미지를 업데이트
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
