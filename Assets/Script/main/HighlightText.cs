using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class HighlightText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public UnityEngine.UI.Text textComponent; // 적용할 Text 컴포넌트 연결
    public Color highlightColor = Color.red; // 텍스트를 강조할 때 사용할 색상
    public AudioClip hoverSound; // 마우스 오버 시 재생할 효과음

    private Color originalColor; // 텍스트 원래 색상을 저장 변수
    private AudioSource audioSource; // 오디오 소스를 참조 변수

    void Start() 
    {
        originalColor = textComponent.color; // 텍스트의 현재 색상을 원래 색상으로 저장
        audioSource = gameObject.AddComponent<AudioSource>(); // 게임 오브젝트에 AudioSource 컴포넌트를 추가, 참조 저장
    }

    public void OnPointerEnter(PointerEventData eventData) // 마우스 포인터가 UI 요소에 들어갈 때 호출되는 메서드
    {
        textComponent.color = highlightColor; // 강조 색상으로 변경
        PlaySound(hoverSound); // 효과음재생
    }

    public void OnPointerExit(PointerEventData eventData) // 마우스 포인터가 UI 요소에서 나갈 때 호출
    {
        textComponent.color = originalColor; // 텍스트 색상을 원래 색상으로 되돌림
    }

    private void PlaySound(AudioClip clip){ // 오디오 클립 재생 메서드
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); // 오디오 소스를 통해 일회성으로 클립을 재생
        }
    }
}
