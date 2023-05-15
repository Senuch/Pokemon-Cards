using Controller;
using UnityEngine;
using View;

namespace Manager
{
    // ReSharper disable once IdentifierTypo
    public class Gamemanager : MonoBehaviour
    {
        [SerializeField] private CardListMenuView view;
        private CardListMenuController _controller;

        private void Awake()
        {
            _controller = new CardListMenuController(view);
        }
    }
}