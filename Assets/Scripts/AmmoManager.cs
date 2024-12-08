using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tankfender
{
    public class AmmoManager : MonoBehaviour
    {
        [SerializeField] private List<AmmoUIElement> playerAmmo;
        private int currentPlayerAmmo;
        private int currentMaxPlayerAmmo;
        private int maxPlayerAmmo;
        private int bulletQuantity;


        public void SetParameters(int currentPlayerAmmo, int currentMaxPlayerAmmo)
        {
            this.maxPlayerAmmo = playerAmmo.Count;
            SetCurrentMaxAmmo(currentMaxPlayerAmmo);
            SetCurrentAmmo(currentPlayerAmmo);

        }

        public void SetCurrentAmmo(int newPlayerHP)
        {
            this.currentPlayerAmmo = newPlayerHP;
            for (int i = 0; i < currentMaxPlayerAmmo; i++)
            {
                if (currentPlayerAmmo > i)
                {
                    playerAmmo[i].Full();
                }
                else
                {
                    playerAmmo[i].Empty();
                }
            }
        }

        public void SetCurrentMaxAmmo(int currentMaxPlayerAmmo)
        {
            this.currentMaxPlayerAmmo = currentMaxPlayerAmmo;
            for (int i = 0; i < playerAmmo.Count - 1; i++)
            {
                if (currentMaxPlayerAmmo >= i + 1)
                {
                    playerAmmo[i].gameObject.SetActive(true);
                }
                else
                {
                    playerAmmo[i].gameObject.SetActive(false); ;
                }
            }
        }

        public void AddOneMoreMaxAmmo()
        {
            if (currentMaxPlayerAmmo + 1 <= maxPlayerAmmo)
            {
                playerAmmo[currentMaxPlayerAmmo].gameObject.SetActive(true);
                this.currentMaxPlayerAmmo += 1;
            }
            else
            {
                Debug.Log("Maxima cantidad de balas alcanzada");
            }
        }

        public int GetAmmoQuantity()
        {
            return playerAmmo.Count;
        }

        public void ReduceAmmoBy1()
        {
            SetCurrentAmmo(currentPlayerAmmo - 1);
        }

        public void Reload()
        {
            SetCurrentAmmo(currentMaxPlayerAmmo);
        }
    }
}
