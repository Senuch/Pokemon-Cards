using System.Collections.Generic;
using Core.Networking;
using Model;
using UnityEngine;
using View;

namespace Controller
{
    public class CardListMenuController
    {
        private readonly CardListMenuView _menuView;
        private readonly List<Pokemon> _pokemonData = new();
        private int _currentPage = 1;

        public CardListMenuController(CardListMenuView menuView)
        {
            _menuView = menuView;
            _menuView.next.onClick.AddListener(OnNext);
            _menuView.previous.onClick.AddListener(OnPrevious);
            _menuView.next.interactable = false;
            _menuView.previous.interactable = false;

            _menuView.Init();
            LoadNetworkData();
        }

        private void LoadNetworkData()
        {
            // TODO: Replace with named based get requests.
            for (int i = 1; i <= 120; i++)
            {
                #pragma warning disable CS4014
                RestClient.Instance.Get(
                #pragma warning restore CS4014
                    $"https://pokeapi.co/api/v2/pokemon/{i}",
                    (OnDataLoaded),
                    new HttpGetRequestHandler());
            }
        }

        private void OnDataLoaded(IResponse response)
        {
            Debug.Log("Request Success");
        }

        private void OnNext()
        {
            throw new System.NotImplementedException();
        }

        private void OnPrevious()
        {
            throw new System.NotImplementedException();
        }
    }
}