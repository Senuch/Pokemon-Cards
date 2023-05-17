using System.Collections.Generic;
using System.Linq;
using Core.Pooling;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CardListMenuView : MonoBehaviour
    {
        public Button next;
        public Button previous;
        public GameObject pokemonCardPrefab;
        public GameObject pokeCardMenuDisplay;
        public TextMeshProUGUI internetStatusText;

        private ResourcePool<IResource<Pokemon>> _cardViewResourcePool;
        private readonly List<IResource<Pokemon>> _renderedResources = new();

        public void Init()
        {
            _cardViewResourcePool = new ResourcePool<IResource<Pokemon>>(
                Configuration.Configurations.PerPageCardCount,
                pokeCardMenuDisplay,
                pokemonCardPrefab);
        }

        public void RenderView(List<Pokemon> data)
        {
            if (data.Count == 0)
            {
                return;
            }

            int startDataIndex = 0;
            if (data.Count < _renderedResources.Count)
            {
                CleanupView();
            }

            foreach (var card in _renderedResources.TakeWhile(card => startDataIndex != data.Count))
            {
                card.Refresh(data[startDataIndex++]);
                card.EnableView();
            }

            for (int i = startDataIndex; i < data.Count; i++)
            {
                var cardView = _cardViewResourcePool.GetResource();
                cardView.Init(data[i]);
                cardView.EnableView();

                _renderedResources.Add(cardView);
            }
        }

        private void CleanupView()
        {
            foreach (var poolResource in _renderedResources)
            {
                poolResource.DisableView();
            }
        }
    }
}