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
        private readonly Queue<HttpRequest> _retryRequests = new();
        private int _currentPage = 1;

        public CardListMenuController(CardListMenuView menuView)
        {
            _menuView = menuView;
            _menuView.next.onClick.AddListener(OnNext);
            _menuView.previous.onClick.AddListener(OnPrevious);
            _menuView.next.interactable = false;
            _menuView.previous.interactable = false;

            Internet.ConnectionStatusChangeEvent(OnConnectionStatusChanged);

            _menuView.Init();
            LoadNetworkData();
        }

        private void LoadNetworkData()
        {
            // TODO: Replace with name based get requests.
            for (int i = 1; i <= 5; i++)
            {
                GetPokemonData($"https://pokeapi.co/api/v2/pokemon/{i}");
            }
        }

        private void OnConnectionStatusChanged(params object[] args)
        {
            if (Internet.IsConnected)
            {
                while (_retryRequests.Count > 0)
                {
                    HttpRequest request = _retryRequests.Dequeue();
                    GetPokemonData(request.Uri.AbsoluteUri);
                }
            }
        }

        private void GetPokemonData(string url)
        {
            // TODO: Validate name
#pragma warning disable CS4014
            RestClient.Get(
#pragma warning restore CS4014
                url,
                (OnRequestCompleted),
                new HttpPreFlightRequestHandler(),
                new HttpGetRequestHandler(),
                new HttpPostFlightRequestHandler());
        }

        private void OnRequestCompleted(IResponse response)
        {
            var httpResponse = response as HttpResponse;
            if (httpResponse is { Success: false })
            {
                if (httpResponse.Retry)
                {
                    _retryRequests.Enqueue(httpResponse.Request);
                }
                return;
            }

            RenderPokemonData(httpResponse!.Data);
        }

        private void RenderPokemonData(string data)
        {
            Pokemon pokemon = JsonConvert.DeserializeObject<Pokemon>(data);
            _pokemonData.Add(pokemon);
            _pokemonData.Sort((a, b) => b.CompareTo(a));
            int renderRange = _pokemonData.Count < 28 ? _pokemonData.Count : 28;
            _menuView.RenderView(_pokemonData.GetRange(0, renderRange));

            if (_pokemonData.Count > 28)
            {
                _menuView.next.interactable = true;
            }
        }

        private void OnNext()
        {
            int nextPage = _currentPage + 1;
            int renderCount = nextPage * 28 <= _pokemonData.Count ? 28 : _pokemonData.Count - (_currentPage * 28);
            _menuView.RenderView(_pokemonData.GetRange(_currentPage * 28, renderCount));
            _currentPage += 1;

            if (_currentPage * 28 >= _pokemonData.Count)
            {
                _menuView.next.interactable = false;
            }

            _menuView.previous.interactable = true;
        }

        private void OnPrevious()
        {
            int prevPage = _currentPage - 1;
            int renderCount = prevPage * 28;
            _menuView.RenderView(_pokemonData.GetRange( prevPage is 1 ? 0 : renderCount - 28, 28));
            _currentPage -= 1;

            if (_currentPage == 1)
            {
                _menuView.previous.interactable = false;
            }

            _menuView.next.interactable = true;
        }
    }
}