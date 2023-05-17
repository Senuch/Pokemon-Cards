using Core.Pooling;
using Model;
using TMPro;
using UnityEngine;
#pragma warning disable CS0108, CS0114

namespace View
{
    public class CardView : MonoBehaviour, IResource<Pokemon>
    {
        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI baseExperience;

        public void Init(Pokemon data)
        {
            UpdateData(data);
            DisableView();
        }

        public void Refresh(Pokemon data)
        {
            UpdateData(data);
        }

        public void EnableView()
        {
            this.gameObject.SetActive(true);
        }

        public void DisableView()
        {
            this.gameObject.SetActive(false);
        }

        private void UpdateData(Pokemon data)
        {
            name.text = data.Name;
            baseExperience.text = $"BASE EXP => " + data.BaseExperience;
        }
    }
}