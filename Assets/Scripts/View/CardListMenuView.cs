using System.Collections.Generic;
using Core.Pooling;
using Model;
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

        private ResourcePool<IResource<Pokemon, CardView>> _cardViewResourcePool;
        private readonly List<IResource<Pokemon, CardView>> _renderedResources = new();

        private const int RenderLimit = 28;

        public void Init()
        {
            _cardViewResourcePool = new ResourcePool<IResource<Pokemon, CardView>>(
                RenderLimit,
                pokeCardMenuDisplay,
                pokemonCardPrefab);
        }

        public void RenderView(List<Pokemon> data) { }

        private void CleanupView() { }
    }
}