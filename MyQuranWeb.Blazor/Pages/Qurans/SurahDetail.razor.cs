using Microsoft.AspNetCore.Components;
using MyQuranWeb.Domain.Models;

namespace MyQuranWeb.Blazor.Pages.Qurans
{
    public partial class SurahDetail
    {

        [Parameter]
        public int? Id { get; set; }

        private int? AyahId;
        private IList<Surah> SurahList = new List<Surah>();
        private IList<Ayah> AyahList = new List<Ayah>();

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Surat";
            SurahList = (await UnitOfWork.Surahs.GetAll()).ToList();
            AyahList = (await UnitOfWork.Ayahs.GetBySurahID(Id.Value)).ToList();
        }
    }
}
