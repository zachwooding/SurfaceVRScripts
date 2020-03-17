using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

/*
 * Modified by Zach Wooding
 * 
 * */
namespace Valve.VR.InteractionSystem.Sample
{
    [RequireComponent(typeof(Interactable))]
    public class GunController : MonoBehaviour
    {
        [SteamVR_DefaultActionSet("gun")]
        public SteamVR_ActionSet actionSet;

        [SteamVR_DefaultAction("Shoot", "gun")]
        public SteamVR_Action_Boolean a_shoot;

        private Interactable interactable;

        private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

        public AudioClip clip;
        public AudioSource audioSource;
        public Animator anim;
        public Transform gunBarrelTrans;
        public Transform playerRot;

        private Transform gunRot;
        private TextMesh textMesh;
        private Vector3 oldPosition;
        private Quaternion oldRotation;
        private float attachTime;

        public GameObject muzzleFlash;


        void Awake()
        {
            textMesh = GetComponentInChildren<TextMesh>();
            textMesh.text = "No Hand Hovering";

            interactable = this.GetComponent<Interactable>();
        }

        private void OnHandHoverBegin(Hand hand)
        {
            textMesh.text = "Hovering hand: " + hand.name;
        }

        private void OnHandHoverEnd(Hand hand)
        {
            textMesh.text = "No Hand Hovering";
        }

        private void HandHoverUpdate(Hand hand)
        {
            GrabTypes startingGrabType = hand.GetGrabStarting();
            bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

            if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
            {
                // Save our position/rotation so that we can restore it when we detach
                oldPosition = transform.position;
                oldRotation = transform.rotation;

                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                hand.HoverLock(interactable);

                // Attach this object to the hand
                hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            }
            else if (isGrabEnding)
            {
                // Detach this object from the hand
                hand.DetachObject(gameObject);

                // Call this to undo HoverLock
                hand.HoverUnlock(interactable);

                // Restore position/rotation
                transform.position = oldPosition;
                transform.rotation = oldRotation;
            }
        }

        private void OnAttachedToHand(Hand hand)
        {
            textMesh.text = "Attached to hand: " + hand.name;
            attachTime = Time.time;
        }

        private void OnDetachedFromHand(Hand hand)
        {
            textMesh.text = "Detached from hand: " + hand.name;
        }

        private void HandAttachedUpdate(Hand hand)
        {
            textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + (Time.time - attachTime).ToString("F2");
        }

        private void Start()
        {
            //Setting up the gun
            gunRot = GetComponent<Transform>();
            interactable = GetComponent<Interactable>();
            interactable.activateActionSetOnAttach = actionSet;
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = clip;
        }

        private void Update()
        {
            bool b_shoot = false;
            //when holding and trigger is pulled shoot the gun
            if (interactable.attachedToHand)
            {
                SteamVR_Input_Sources hand = interactable.attachedToHand.handType;

                b_shoot = a_shoot.GetStateDown(hand);
            }
            else
            {
                
                //gunRot.SetPositionAndRotation(new Vector3(playerRot.position.x + 0.2f, 0.8f, 0.4f), playerRot.rotation);
                gunRot.SetPositionAndRotation(new Vector3(playerRot.position.x + 0.1f, playerRot.position.y - 0.6f, playerRot.position.z + 0.3f), playerRot.rotation);
               // gunRot.Rotate(new Vector3(80, playerRot.rotation.y, 0));
            }

            if (b_shoot)//actually shooting the gun
            {
                audioSource.Play();
                anim.Play("1911Animation");
                StartCoroutine(MuzzleFlashSpawn()); 
                ShootGun();
            }
        }
         IEnumerator MuzzleFlashSpawn()
        {
            muzzleFlash.SetActive(true);
            muzzleFlash.GetComponent<ParticleSystem>().Play();
            yield return new WaitForSeconds(0.1f);
            muzzleFlash.SetActive(false);
        }
        //Raycast from the gun
        private void ShootGun()
        {
            
            RaycastHit hit;

            if (Physics.Raycast(gunBarrelTrans.position, gunBarrelTrans.forward, out hit))
            {
                Debug.DrawRay(gunBarrelTrans.position, gunBarrelTrans.forward);
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    print("HitEnemy");
                    hit.collider.gameObject.GetComponent<StateController>().TakeDamage();
                   
                }
            }
            
        }
    }
}

