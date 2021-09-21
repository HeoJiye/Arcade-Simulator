using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviourPunCallbacks, IPunObservable
{
    public PhotonView PV;
    public Rigidbody2D RB;
    public Animator AN;
    public Text NickNameText;
    public Image emotionView;

    public Sprite[] emotionImage = new Sprite[25];

    Transform cam;

    Vector3 curPos;

    int currentEmotion = 0;

    void Awake()
    {
        // 다른 씬으로 넘어가도 사라지지 않게 한다.
        DontDestroyOnLoad(gameObject);

        // 닉네임 불러오기
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;

        if(PV.IsMine) {
            cam = GameObject.Find("Main Camera").GetComponent<Transform>();
            gameObject.tag = "LocalPlayer";
        }
    }
    
    void Update()
    {
        // localPlayer
        if(PV.IsMine) {
            float deltaX = JoyStick.deltaX, deltaY = JoyStick.deltaY;
            int direction;

            if(deltaX == 0 && deltaY == 0)
                AN.SetBool("Move", false);
            else {
                AN.SetBool("Move", true);

                if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
                    if(deltaX > 0) direction = 1;
                    else direction = 2;
                else 
                    if(deltaY > 0) direction = 3;
                    else direction = 0;

                AN.SetInteger("Direction", direction);
                RB.MovePosition(RB.position + new Vector2(deltaX, deltaY) * 1/60f);
                
                cam.position = transform.position;
                cam.position += new Vector3(0, 0, -10);
            }

            PV.RPC("AnimRPC", RpcTarget.All, AN.GetBool("Move"), AN.GetInteger("Direction"));
        }
        // otherPlayer
            // 멀리 있을 때는 한번에 이동 (딜레이 방지)
            // 가까이 있을 때는 자연스럽게 이동
        else if ((transform.position - curPos).sqrMagnitude >= 500) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    void FixedUpdate()
    {
        // Main Camera가 localPlayer 따라다니기
        if(PV.IsMine) {
            cam.position = transform.position;
            cam.position += new Vector3(0, 0, -10);
        }

        if(PV.IsMine) {
            currentEmotion = ArcadeManager.currentEmotion;
            PV.RPC("emotionRPC", RpcTarget.All, currentEmotion);
        }
        if(currentEmotion == 0)
            emotionView.color = new Color(1f, 1f, 1f, 0f);
        else {
            emotionView.color = new Color(1f, 1f, 1f, 1f);
            emotionView.sprite = emotionImage[currentEmotion];
        }
    }

    public void destory() {
        PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
    }

    // PunRPC -> 다른 플레이어의 정보를 가져온다.

    // 애니메이션 정보 전달
    [PunRPC]
    void AnimRPC(bool move, int direction)
    {
        AN.SetBool("Move", move);
        AN.SetInteger("Direction", direction);
    }

    // 감정 말풍선 전달
    [PunRPC]
    void emotionRPC(int index)
    {
        currentEmotion = index;
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    // 위치 정보 공유
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
            stream.SendNext(transform.position);
        else
            curPos = (Vector3)stream.ReceiveNext();
    }

}
