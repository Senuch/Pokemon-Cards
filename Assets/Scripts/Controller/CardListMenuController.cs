using System.Collections.Generic;
using System.Linq;
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

        public CardListMenuController(CardListMenuView menuView, List<string> input)
        {
            _menuView = menuView;
            _menuView.next.onClick.AddListener(OnNext);
            _menuView.previous.onClick.AddListener(OnPrevious);
            _menuView.next.interactable = false;
            _menuView.previous.interactable = false;

            Internet.ConnectionStatusChangeEvent(OnConnectionStatusChanged);
            Init(input);
        }

        private void Init(List<string> input)
        {
            List<string> sanitizedInput = GetSanitizedInput(input);
            if (sanitizedInput.Count is 0)
            {
                _menuView.DisplayMessage("Provided input is invalid!", false);
            }
            else
            {
                _menuView.Init();
                LoadNetworkData(sanitizedInput);
            }
        }

        private List<string> GetSanitizedInput(List<string> input)
        {
            var pokemonNames = new HashSet<string>();
            if (input.Count is 0)
            {
                _menuView.DisplayMessage("No input provided!", false);
                return new List<string>();
            }

            foreach(string pokemonName in input)
            {
                if (pokemonName is null) continue;
                var name = pokemonName.Trim();
                name = name.ToLower();
                bool isValid = name.Length > 0 && name.All(char.IsLetter) && !pokemonNames.Contains(name);
                if (isValid)
                {
                    pokemonNames.Add(name);
                }
            }

            return pokemonNames.ToList();
        }

        private void LoadNetworkData(List<string> input)
        {
            foreach (string pokemonName in input)
            {
                GetPokemonData($"https://pokeapi.co/api/v2/pokemon/{pokemonName}");
            }
        }

        private void OnConnectionStatusChanged(params object[] args)
        {
            _menuView.UpdateConnectionStatus(Internet.IsConnected);
            if (!Internet.IsConnected) return;
            while (_retryRequests.Count > 0)
            {
                HttpRequest request = _retryRequests.Dequeue();
                GetPokemonData(request.Uri.AbsoluteUri);
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
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(data);

            _pokemonData.Add(pokemon);
            _pokemonData.Sort((a, b) => b.CompareTo(a));
            int renderRange = _pokemonData.Count < Configuration.Configurations.PerPageCardCount
                ? _pokemonData.Count
                : Configuration.Configurations.PerPageCardCount;
            _menuView.RenderView(_pokemonData.GetRange(0, renderRange));

            if (_pokemonData.Count > Configuration.Configurations.PerPageCardCount)
            {
                _menuView.next.interactable = true;
            }
        }

        private void OnNext()
        {
            int nextPage = _currentPage + 1;
            int renderCount = nextPage * Configuration.Configurations.PerPageCardCount <= _pokemonData.Count
                ? Configuration.Configurations.PerPageCardCount
                : _pokemonData.Count - (_currentPage * Configuration.Configurations.PerPageCardCount);
            _menuView.RenderView(_pokemonData.GetRange(_currentPage * Configuration.Configurations.PerPageCardCount, renderCount));
            _currentPage += 1;

            if (_currentPage * Configuration.Configurations.PerPageCardCount >= _pokemonData.Count)
            {
                _menuView.next.interactable = false;
            }

            _menuView.previous.interactable = true;
        }

        private void OnPrevious()
        {
            int prevPage = _currentPage - 1;
            int renderCount = prevPage * Configuration.Configurations.PerPageCardCount;
            _menuView.RenderView(_pokemonData.GetRange(
                prevPage is 1 ? 0 : renderCount - Configuration.Configurations.PerPageCardCount,
                Configuration.Configurations.PerPageCardCount));
            _currentPage -= 1;

            if (_currentPage == 1)
            {
                _menuView.previous.interactable = false;
            }

            _menuView.next.interactable = true;
        }
    }
}