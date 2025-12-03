using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using MyQuranWeb.Blazor.Services;
using MyQuranWeb.Blazor.Shared;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Domain.Models.Hadiths;

namespace MyQuranWeb.Blazor.Pages.Qurans
{
    public partial class SurahDetail
    {

        [Parameter]
        public int? Id { get; set; }

        [Inject]
        private CopyService CopyService { get; set; }

        private int? AyahId;
        private IList<Surah> SurahList = new List<Surah>();
        private IList<Ayah> AyahList = new List<Ayah>();
        private Surah Surah;
        private string hideTransliterasi = "";
        private string hideTerjemahan = "";
        private string fontArab = "arab";
        private string bismillah = "بِسْمِ اللّٰهِ الرَّحْمٰنِ الرَّحِيْمِ";
        private string tafsir = "Kemenag";
        private string bookmarks = "fa-bookmark-o";

        protected override async Task OnInitializedAsync()
        {
            PageTitle = "Surat";
            SurahList = (await UnitOfWork.Surahs.GetAll()).ToList();
            AyahList = (await UnitOfWork.Ayahs.GetBySurahID(Id!.Value)).ToList();
            Surah = (await UnitOfWork.Surahs.GetByID(Id!.Value));

            foreach (var ayah in AyahList)
            {
                IconNameLastAyahs.Add(ayah.Id, IconName.Sticky);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var settingTransliterasi = await localStorage.GetItemAsync<string>("transliterasiSetting");            
            if (!string.IsNullOrWhiteSpace(settingTransliterasi) && settingTransliterasi == "0")
            {
                hideTransliterasi = "hide text-hide";
            }

            var settingTerjemahan = await localStorage.GetItemAsync<string>("terjemahanSetting");
            if (!string.IsNullOrWhiteSpace(settingTerjemahan) && settingTerjemahan == "0")
            {
                hideTerjemahan = "hide text-hide";
            }

            var settingRasm = await localStorage.GetItemAsync<string>("rasmSetting");
            if (!string.IsNullOrWhiteSpace(settingRasm) && settingRasm == "1")
            {
                fontArab = "arabUthmani";
                bismillah = "بِسۡمِ ٱللَّهِ ٱلرَّحۡمَٰنِ ٱلرَّحِيمِ";
            }
            var settingTafsir = await localStorage.GetItemAsync<string>("tafsirSetting");
            if (!string.IsNullOrWhiteSpace(settingTafsir) && settingTafsir == "1")
            {
                tafsir = "Al-Jalalain";
            }

            bookmarks = "fa-bookmark-o";
            //foreach (var ayah in AyahList)
            //{
            //    if (!string.IsNullOrEmpty(Task.Run(() => localStorage.GetItemAsStringAsync(ayah.Id.ToString()).Result).Result))
            //    {
            //        bookmarks = "fa-bookmark";
            //    }
            //}

            var lastAyah = await localStorage.GetItemAsStringAsync("lastAyah");
            if (!string.IsNullOrEmpty(lastAyah))
            {
                IconNameLastAyahs[Convert.ToInt32(lastAyah.Split('|')[3])] = IconName.StickyFill;
            }
        }

        private async void ScrollToAyah(int ayahId = 0)
        {            
            await JSRuntime.InvokeVoidAsync("scrollToAyah", ayahId.ToString());
        }

        private Modal modal =  default!;
        private async Task OnShowModalClick()
        {
            await modal.ShowAsync();
        }

        private async Task OnHideModalClick()
        {
            await modal.HideAsync();
        }

        private string? tafsirText;

        private async Task ShowTafsirComponent(int id)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("Id", id);
            parameters.Add("OnCopyCallbackClick", EventCallback.Factory.Create<MouseEventArgs>(this, CopyTafsir));

            await modal.ShowAsync<TafsirComponent>(title: "Tafsir", parameters: parameters);
        }

        private async Task OnCopyTafsirClick()
        {
            await ClipboardService.WriteTextAsync(CopyService.Value);
            await ShowMessage("Salin Tafsir", "Salin tafsir berhasil.");
        }
        private void CopyTafsir(MouseEventArgs e) => tafsirText = "Terjemah dari kemenag.";
    }
}
