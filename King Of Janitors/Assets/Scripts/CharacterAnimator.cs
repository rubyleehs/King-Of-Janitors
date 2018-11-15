using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterAnimator : MonoBehaviour
{
    [Header("Animation")]
    public Sprite[] frames;
    public int idleFrame;//should be within move frames
    public int deathFrame;
    public Vector2Int moveFrames;
    public float timePerFrame;

    [Header("Audio")]
    public bool[] playFootstepFrames;
    public float footstepVolume;
    public AudioClip footstepClip;

    public GameObject audioClipPlayerPrefab;
    private SpriteRenderer spriteRenderer;

    private bool isMoving;

    private float t;
    private int currentFrame; // 4 states 0 - 3

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Initialize();
    }

    public void Move(Vector2 position, float dt)
    {
        if (!isMoving)
        {
            isMoving = true;
            SetFrame(((currentFrame + 1) - moveFrames.x) % (moveFrames.y + 1 - moveFrames.x) + moveFrames.x, position);
        }

        t += dt;
        if (t >= timePerFrame)
        {
            t = 0;
            SetFrame(((currentFrame + 1) - moveFrames.x) % (moveFrames.y + 1- moveFrames.x) + moveFrames.x, position);
        }
    }

    public void Halt()
    {
        isMoving = false;
        SetFrame(idleFrame, Vector2.zero);
    }

    public void SetFrame(int f, Vector2 position)
    {
        currentFrame = f;
        spriteRenderer.sprite = frames[f];

        if (playFootstepFrames[f]) AudioClipPlayer.Play(footstepClip,Random.Range(0.9f,1.1f),footstepVolume,position,audioClipPlayerPrefab);
    }

    public void Initialize()
    {
        Halt();
    }

}