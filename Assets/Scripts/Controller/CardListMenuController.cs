using System.Collections.Generic;
using Core.Networking;
using Model;
using Newtonsoft.Json;
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
            // TODO: Replace with name based get requests.
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
            if(!response.Success) return;

            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>((string)response.Context["DATA"]);
            _pokemonData.Add(pokemon);
            _pokemonData.Sort((a, b) => b.CompareTo(a));
            int renderRange = _pokemonData.Count < 28 ? _pokemonData.Count : 28;
            _menuView.RenderView(_pokemonData.GetRange(0, renderRange));
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