using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class AirHockey_PlayerMove : MonoBehaviourPunCallbacks
{
     public PhotonView PV;
     public SpriteRenderer SR;

     public Sprite player;
     public Sprite opponent;

     bool wasJustClicked = true;
     bool canMove;

     Rigidbody2D rb;
     Vector2 startingPosition;

     public Transform BoundaryHolder;

     Rigidbody2D puck;

    Boundary playerBoundary;

    Collider2D playerCollider;

    Vector2 curPos;
    Vector2 puckPos;

    string target;

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position;
        playerCollider = GetComponent<Collider2D>();

        puck = GameObject.Find("Puck").GetComponent<Rigidbody2D>();

        if(PV.IsMine) {
            SR.sprite = player;

            rb.MovePosition(new Vector2(0f, -5f));
            startingPosition = rb.position;

            BoundaryHolder = GameObject.Find("PlayerBoundaryHolder").GetComponent<Transform>();

            playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
                                          BoundaryHolder.GetChild(1).position.y,
                                          BoundaryHolder.GetChild(2).position.x,
                                          BoundaryHolder.GetChild(3).position.x);

            PV.RPC("setTarget", RpcTarget.All, AirHockeyManager.opponentPlayer);
        }
        else {
            rb.MovePosition(new Vector2(0f, 5f));

            SR.sprite = opponent;
        }
    }

    void Start() => target = PhotonNetwork.LocalPlayer.NickName;
    

    // Update is called once per frame
    void Update()
    {
        if(target != PhotonNetwork.LocalPlayer.NickName) Destroy(gameObject);

        if(PV.IsMine) {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (wasJustClicked)
                {
                    wasJustClicked = false;

                    if (playerCollider.OverlapPoint(mousePos))
                    {
                        canMove = true;
                    }
                    else
                    {
                        canMove = false;
                    }
                }

                if (canMove)
                {
                    Vector2 clampedMousePos = new Vector2(Mathf.Clamp(mousePos.x, playerBoundary.Left,
                                                                      playerBoundary.Right),
                                                          Mathf.Clamp(mousePos.y, playerBoundary.Down,
                                                                      playerBoundary.Up));
                    rb.MovePosition(clampedMousePos);
                }
            }
            else
            {
                wasJustClicked = true;
            }

            PV.RPC("setPos", RpcTarget.All, rb.position);
        }
        else 
            rb.MovePosition(curPos);

        if(string.Compare(PhotonNetwork.LocalPlayer.NickName, AirHockeyManager.opponentPlayer) > 0)
            PV.RPC("setPuckPos", RpcTarget.All, puck.position);
        else puck.MovePosition(puckPos);
    }

    public void ResetPosition()
    {
        rb.position = startingPosition;
    }

    public void destory() {
        PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void setTarget(string nickName) => target = nickName;

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    [PunRPC]
    void setPos(Vector2 pos) {
        Vector2 t = new Vector2(0, 0);
        t -= pos;
        curPos = t;
    }

    [PunRPC]
    void setPuckPos(Vector2 pos) {
        Vector2 t = new Vector2(0, 0);
        t -= pos;
        puckPos = t; 
    }
}
