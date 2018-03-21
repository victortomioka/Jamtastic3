using System.Collections;
using System.Collections.Generic;
using Carnapunk.SaintCarnage.Components;
using UnityEngine;

namespace Carnapunk.SaintCarnage.Components
{
    public class GunHolder : MonoBehaviour
    {
        public int selectedSlot = -1;
        public Transform[] modelSlots;

        public GunSlot[] slots;
        

        public Gun SelectedGun
        {
            get
            {
                if (selectedSlot < 0 || slots.Length == 0)
                    return null;

                return slots[selectedSlot].gun;
            }
        }

        public delegate void SelectedGunAction(int slotIndex, Gun selectedGun);
        public event SelectedGunAction OnSelectedGun;

        private void Start()
        {
            slots = transform.GetComponentsInChildren<GunSlot>();
            SelectSlot();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetSlot(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetSlot(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetSlot(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetSlot(3);
            }

            if (Input.mouseScrollDelta.y > 0)
            {
                NextSlot();
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                PreviousSlot();
            }
        }

        public void NextSlot()
        {
            int next = selectedSlot + 1;

            while (next != selectedSlot)
            {
                if (IsSlotAvailable(next))
                {
                    selectedSlot = next;
                    SelectSlot();
                    return;
                }

                if (next >= slots.Length)
                    next = 0;
                else
                    next++;
            }
        }

        public void PreviousSlot()
        {
            int previous = selectedSlot - 1;

            while (previous != selectedSlot)
            {
                if (IsSlotAvailable(previous))
                {
                    selectedSlot = previous;
                    SelectSlot();
                    return;
                }

                if (previous < 0)
                    previous = slots.Length - 1;
                else
                    previous--;
            }
        }

        public void SetSlot(int slotIndex)
        {
            if (IsSlotAvailable(slotIndex) && selectedSlot != slotIndex)
            {
                selectedSlot = slotIndex;
                SelectSlot();
            }
        }

        public void Add(Gun newGun, GameObject gunModel)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                GunSlot slot = slots[i];

                if (slot.type == newGun.stats.type)
                {
                    slot.SetGun(newGun);

                    gunModel.transform.SetParent(modelSlots[i]);
                    gunModel.transform.localPosition = Vector3.zero;
                    gunModel.transform.localRotation = Quaternion.identity;
                    gunModel.transform.localScale = Vector3.one;

                    GameManager.Instance.SetGunSlotIcon(i, slot);
                    SetSlot(i);
                    return;
                }
            }
        }

        private bool IsSlotAvailable(int slotIndex)
        {
            return
                slots.Length > 0 &&
                slotIndex >= 0 &&
                slotIndex < slots.Length &&
                !slots[slotIndex].IsEmpty;
        }

        private void SelectSlot()
        {
            if(selectedSlot == -1)
                return;

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].gameObject.SetActive(i == selectedSlot);
                modelSlots[i].gameObject.SetActive(i == selectedSlot);
            }

            if (OnSelectedGun != null)
                OnSelectedGun(selectedSlot, SelectedGun);
        }
    }
}