using Core.Pooling;
using Model;
using TMPro;
using UnityEngine;

namespace View
{
    public class CardView : MonoBehaviour, IResource<Pokemon, CardView>
    {
        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI baseExperience;

        public ResourcePool<IResource<Pokemon, CardView>> PoolInstance { get; set; }

        public void Init(Pokemon data, ResourcePool<IResource<Pokemon, CardView>> pool)
        {
            PoolInstance = pool;
            UpdateData(data);
            DisableView();
        }

        public void PoolBack()
        {
            DisableView();
            PoolInstance.PoolBack(this);
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