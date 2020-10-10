using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JellySpriteReferencePoint : MonoBehaviour
{
    public GameObject ParentJellySprite { get; set; }
    public bool SendCollisionMessages { get { return m_SendCollisionMessages; } set { m_SendCollisionMessages = value; } }
    public int Index { get; set; }

    JellySprite.JellyCollision m_JellyCollision = new JellySprite.JellyCollision();
    JellySprite.JellyCollision2D m_JellyCollision2D = new JellySprite.JellyCollision2D();

    JellySprite.JellyCollider m_JellyCollider = new JellySprite.JellyCollider();
    JellySprite.JellyCollider2D m_JellyCollider2D = new JellySprite.JellyCollider2D();

    bool m_SendCollisionMessages = true;

    [SerializeField]
    public List<bool> isTouch = new List<bool> { false, false, false, false, false };

    public enum TOUCHTYPE
    {
        LIGHT = 0,
        PLAYER,
        HEAVY,
        GROUND,
        WALL,
    }

    public List<GameObject> attachItem = new List<GameObject>();
    public List<bool> itemIsAttach = new List<bool>();
    public GameObject attachPlayer;

    public JellySpriteReferencePoint()
    {
        m_JellyCollision.ReferencePoint = this;
        m_JellyCollision2D.ReferencePoint = this;
        m_JellyCollider.ReferencePoint = this;
        m_JellyCollider2D.ReferencePoint = this;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            m_JellyCollision.Collision = collision;
            ParentJellySprite.SendMessage("OnJellyCollisionEnter", m_JellyCollision, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            //Self code
            if (collision.gameObject.tag != this.tag && collision.gameObject.name != "floorDetect")
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("heavy"))
                {
                    isTouch[(int)TOUCHTYPE.HEAVY] = true;
                }
                else if (collision.gameObject.tag == "ground")
                {
                    isTouch[(int)TOUCHTYPE.GROUND] = true;
                }
                else if (collision.gameObject.tag == "wall")
                {
                    isTouch[(int)TOUCHTYPE.WALL] = true;
                }
                else if (collision.gameObject.layer == LayerMask.NameToLayer("light"))
                {
                    isTouch[(int)TOUCHTYPE.LIGHT] = true;
                }
                // else if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
                // {
                //     Debug.Log("attach" + collision.gameObject.name);
                // }
                attachItem.Add(collision.gameObject);
                itemIsAttach.Add(true);
            }
            //

            m_JellyCollision2D.Collision2D = collision;
            ParentJellySprite.SendMessage("OnJellyCollisionEnter2D", m_JellyCollision2D, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            m_JellyCollision.Collision = collision;
            ParentJellySprite.SendMessage("OnJellyCollisionExit", m_JellyCollision, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            //Self code
            if (collision.gameObject.tag != this.tag && collision.gameObject.name != "floorDetect")
            {
                if (collision.gameObject.layer == LayerMask.NameToLayer("heavy"))
                {
                    isTouch[(int)TOUCHTYPE.HEAVY] = false;
                }
                else if (collision.gameObject.tag == "ground")
                {
                    isTouch[(int)TOUCHTYPE.GROUND] = false;
                }
                else if (collision.gameObject.tag == "wall")
                {
                    isTouch[(int)TOUCHTYPE.WALL] = false;
                }
                else if (collision.gameObject.layer == LayerMask.NameToLayer("light"))
                {
                    isTouch[(int)TOUCHTYPE.LIGHT] = false;
                }
                attachItem.Remove(collision.gameObject);
                itemIsAttach.RemoveAt((int)TOUCHTYPE.LIGHT);
            }
            //

            m_JellyCollision2D.Collision2D = collision;
            ParentJellySprite.SendMessage("OnJellyCollisionExit2D", m_JellyCollision2D, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            m_JellyCollision.Collision = collision;
            ParentJellySprite.SendMessage("OnJellyCollisionStay", m_JellyCollision, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {

            m_JellyCollision2D.Collision2D = collision;
            ParentJellySprite.SendMessage("OnJellyCollisionStay2D", m_JellyCollision2D, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {

            m_JellyCollider.Collider = collider;
            ParentJellySprite.SendMessage("OnJellyTriggerEnter", m_JellyCollider, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            //Self code
            if (collider.gameObject.tag != this.tag && collider.gameObject.name != "floorDetect")
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("player"))
                {
                    isTouch[(int)TOUCHTYPE.PLAYER] = true;
                    attachPlayer = collider.gameObject;
                }
            }
            //

            m_JellyCollider2D.Collider2D = collider;
            ParentJellySprite.SendMessage("OnJellyTriggerEnter2D", m_JellyCollider2D, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            m_JellyCollider.Collider = collider;
            ParentJellySprite.SendMessage("OnJellyTriggerExit", m_JellyCollider, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            //Self code
            if (collider.gameObject.tag != this.tag && collider.gameObject.tag != "floorDetect")
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("player"))
                {
                    isTouch[(int)TOUCHTYPE.PLAYER] = false;
                    attachPlayer = null;
                }
            }
            //

            m_JellyCollider2D.Collider2D = collider;
            ParentJellySprite.SendMessage("OnJellyTriggerExit2D", m_JellyCollider2D, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            m_JellyCollider.Collider = collider;
            ParentJellySprite.SendMessage("OnJellyTriggerStay", m_JellyCollider, SendMessageOptions.DontRequireReceiver);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (ParentJellySprite && SendCollisionMessages)
        {
            m_JellyCollider2D.Collider2D = collider;
            ParentJellySprite.SendMessage("OnJellyTriggerStay2D", m_JellyCollider2D, SendMessageOptions.DontRequireReceiver);
        }
    }
}
