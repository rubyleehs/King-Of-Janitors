using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraLogic : CameraExtras {

    public static Vector2 mousePos;

    [Header("Positioning")]
    public float z;
    public float moveLerpSpeed;

    [Header("Kick")]
    public Vector2 kick;
    public float kickDiminishSpeed;

    [Header("Player")]
    public Transform player;
    public float playerToMouseRatio;

    [Header("Audio")]
    public float masterVolume;

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        this.transform.position = player.position + Vector3.forward * z;
        AudioListener.volume = masterVolume;
    }

    void FixedUpdate()
    {
        Vector2 lerp = Vector2.LerpUnclamped(player.position, mousePos, playerToMouseRatio);
        transform.position = (Vector3)(Vector2.Lerp(player.position, lerp, Time.fixedDeltaTime * moveLerpSpeed) + kick) + Vector3.forward * z;
    }

    void Update()
    {
        mousePos = GetMouseWorld2DPoint();
        kick -= kick * kickDiminishSpeed * Time.deltaTime;
    }

}
