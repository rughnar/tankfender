using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Tankfender
{
    public class AmmoUIElement : MonoBehaviour
    {
        [SerializeField] private Sprite full;
        [SerializeField] private Sprite empty;
        [SerializeField] private Image image;

        public void Full()
        {
            image.sprite = full;
        }

        public void Empty()
        {
            image.sprite = empty;
        }


    }
}
