using Controller;
using Core.Networking;
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
#pragma warning disable CS4014
            Internet.StartConnectionCheckService();
#pragma warning restore CS4014
            _controller = new CardListMenuController(view);
        }
    }
}