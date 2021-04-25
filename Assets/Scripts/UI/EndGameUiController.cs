using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text), typeof(Animator))]
public class EndGameUiController : MonoBehaviour
{
    [SerializeField] private string EndGameAnimationLabel = "End Game";
    private Animator animator;
    private Text text;

    public UnityEvent onEndGameAnimationEnd;
    private void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
    }

    public void OnShow(string _text)
    {
        text.text = _text;
        text.gameObject.SetActive(true);
        animator.Play(EndGameAnimationLabel);
    }

    private void OnAnimationEnd() => onEndGameAnimationEnd?.Invoke();
}
