using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Idle Animations")]
    public List<Sprite> IdleUp;
    public List<Sprite> IdleDown;
    public List<Sprite> IdleLeft;
    public List<Sprite> IdleRight;

    [Header("Move Animations")]
    public List<Sprite> MoveUp;
    public List<Sprite> MoveDown;
    public List<Sprite> MoveLeft;
    public List<Sprite> MoveRight;

    [Header("Stats")]
    public int MaxFrames = 4;
    public float PlaybackSpeed = .5f;
    private float playback = 0;
    [HideInInspector]
    public SpriteRenderer Sprite;

    public enum PlayState { idleDown, idleUp, idleRight, idleLeft, walkDown, walkUp, walkRight, walkLeft }
    private PlayState Play = PlayState.idleDown;
    private int progress = 0;

    public void StopWalk() => Play = Play >= PlayState.walkDown ? Play - 4 : Play;

    public PlayState GetPlayState() => Play;
    public void SetPlayState(PlayState newState) => Play = newState;

    public void WalkDirection(Vector3 direction)
    {
        if(direction == Vector3.zero)
        {
            if (Play >= PlayState.walkDown)
                Play -= 4;
        }
        else if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x < 0)
                Play = PlayState.walkLeft;
            else
                Play = PlayState.walkRight;
        }
        else
        {
            if (direction.y < 0)
                Play = PlayState.walkDown;
            else
                Play = PlayState.walkUp;
        }
    }

    private void Awake()
    {
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        playback += Time.deltaTime;
        if(playback >= PlaybackSpeed)
        {
            playback = 0;
            switch (Play)
            {
                default:
                case PlayState.idleDown:
                    Sprite.sprite = IdleDown[progress % IdleDown.Count];
                    break;
                case PlayState.idleLeft:
                    Sprite.sprite = IdleLeft[progress % IdleLeft.Count];
                    break;
                case PlayState.idleRight:
                    Sprite.sprite = IdleRight[progress % IdleRight.Count];
                    break;
                case PlayState.idleUp:
                    Sprite.sprite = IdleUp[progress % IdleUp.Count];
                    break;
                case PlayState.walkDown:
                    Sprite.sprite = MoveDown[progress % MoveDown.Count];
                    break;
                case PlayState.walkLeft:
                    Sprite.sprite = MoveLeft[progress % MoveLeft.Count];
                    break;
                case PlayState.walkRight:
                    Sprite.sprite = MoveRight[progress % MoveRight.Count];
                    break;
                case PlayState.walkUp:
                    Sprite.sprite = MoveUp[progress % MoveUp.Count];
                    break;
            }
            progress = (progress+1) % MaxFrames;
        }
    }
}
