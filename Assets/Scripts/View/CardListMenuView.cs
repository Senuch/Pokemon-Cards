using System.Collections.Generic;
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

        public void Init() { }

        public void RenderView(List<Pokemon> data) { }

        private void CleanupView() { }
    }
}