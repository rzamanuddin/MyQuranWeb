using Castle.Core.Internal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using MyQuranWeb.Blazor.Services;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using System;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Metadata;
using Newtonsoft.Json.Linq;
using BlazorBootstrap;

namespace MyQuranWeb.Blazor.Pages
{
    public class PageBase : ComponentBase
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; init; }

        [Inject]
        public Blazored.LocalStorage.ILocalStorageService localStorage { get; init; }

        [Inject] 
        public IOptions<AppSettingOption> AppSettingOption { get; init;}

        [Inject]
        protected ClipboardService ClipboardService { get; init; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected SweetAlertService Swal { get; set; }

        protected Dictionary<int, IconName> IconNameLastAyahs = new Dictionary<int, IconName>();

        protected string? ErrorMessage;
        protected string? PageTitle;
                        
        protected string? FontSizeSetting;
        protected string? MP3UrlSetting;

        protected virtual async Task CopyAyah()
        {
            
        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            string? qariSetting = await localStorage.GetItemAsStringAsync("qariSetting");

            if (!string.IsNullOrWhiteSpace(qariSetting) && qariSetting != "0")
            {
                if (qariSetting == "1")
                {
                    MP3UrlSetting = AppSettingOption.Value.MP3Url1;
                }
                else if (qariSetting == "2")
                {
                    MP3UrlSetting = AppSettingOption.Value.MP3Url2;
                }
            }

            MP3UrlSetting = AppSettingOption.Value.MP3Url;
            AppSettingOption.Value.MP3UrlSetting = AppSettingOption.Value.MP3Url;

            FontSizeSetting = "h2";
            var taskSetting = Task.Run(() => localStorage.GetItemAsStringAsync("ukuranTeksSetting")).Result;
            if (!string.IsNullOrEmpty(taskSetting.Result))
            {
                if (taskSetting.Result == "1")
                {
                    FontSizeSetting = "h1";
                }
            }
        }

        //protected 
        protected async Task ReadFromClipboard()
        {
            try
            {
                await ClipboardService.ReadTextAsync();
            }
            catch
            {
                Console.WriteLine("Cannot read from clipboard");
            }
        }

        protected async Task CopyToClipboard(int index, int ayahID, int surahID, string ayah, string textIndo, string translateIndo)
        {
            // Writing to the clipboard may be denied, so you must handle the exception
            try
            {
                var copiedText = $"Q.S. {surahID}:{index + 1}\n\n{ayah}\n\n{textIndo}\n\n{translateIndo}\n\n{GetUrl()}";
                await ClipboardService.WriteTextAsync(copiedText);
                await ShowMessage("Salin Ayat", $"Q.S. {surahID}:{ayahID} berhasil disalin.");
            }
            catch (Exception ex)
            {
                await ShowError($"Salin ayat error: {ex.Message}");
            }
        }

        protected string GetUrl()
        {
            return $"*Via MyQuranWeb Indonesia\n{NavigationManager.Uri}";
        }

        /*function setCookie(cname, cvalue, exdays) {
    var isValid = checkCookie(cname);
    var expires = "expires=Thu, 01 Jan 1970 00:00:00"
    var bookmarks = "fa fa-bookmark-o";
    var btnBookmark = $("#bookmark" + cname);
    btnBookmark.removeClass();
    if (!isValid)
    {
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        btnBookmark.addClass(bookmarks);

        $.confirm({
            title: 'Bookmark',
            content: "Berhasil menghapus bookmark.",
            autoClose: 'ok|2000',
            buttons: {
                ok: {
                    text: 'OK',
                    action: function () {
                    }
                }
            }
        });
    }
    else
    {
        const d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";

        bookmarks = "fa fa-bookmark"
        btnBookmark.addClass(bookmarks);

        $.confirm({
            title: 'Bookmark',
            content: "Berhasil menambahkan bookmark.",
            autoClose: 'ok|2000',
            buttons: {
                ok: {
                    text: 'OK',
                    action: function () {
                    }
                }
            }
        });
    }
}*/
        protected async Task SetStorage(string name, string value)
        {
            try
            {
                await localStorage.SetItemAsStringAsync(name, value);
                //await JSRuntime.InvokeVoidAsync("alert", "warning");
                //bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?"); // Confirm
                //string prompted = await JsRuntime.InvokeAsync<string>("prompt", "Take some input:"); // Prompt
                await ShowMessage("Bookmark", "Berhasil menambahkan bookmark.");
            }
            catch (Exception ex)
            {
                await ShowError($"Set storage error: {ex.Message}");
            }
        }

        protected async Task SetLastAyah(int surahId, string surahName, int ayahId, int id, int juzId)
        {
            try
            {
                var cValue = $"{surahId}|{surahName}|{ayahId}|{id}|{juzId}";
                var cName = "lastAyah";
                var message = $"Ayat terakhir dibaca berhasil diubah menjadi Q.S. {surahId}. {surahName} Ayat {ayahId}.";

                if (await localStorage.ContainKeyAsync(cName))
                {
                    var splittedText = (await localStorage.GetItemAsStringAsync(cName))!.Split("|");
                    if (splittedText.Length > 2)
                    {
                        SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
                        {
                            Title = "Konfirmasi",
                            Text = $"Ayat terakhir dibaca Q.S. {splittedText[0]}. {splittedText[1]} Ayat {splittedText[2]} akan diganti menjadi Q.S. {surahId}. {surahName}  Ayat {ayahId}. Anda Yakin ?",
                            Icon = SweetAlertIcon.Warning,
                            ShowCancelButton = true,
                            ConfirmButtonText = "Ok",
                            CancelButtonText = "Batal",
                            FocusCancel = true,
                            TimerProgressBar = true,
                            Timer = 10000,
                        });
                        if (result.IsConfirmed)
                        {
                            await localStorage.SetItemAsStringAsync(cName, cValue);
                            // TODO: show toast message
                            await ShowMessage("Terkahir Dibaca", message);
                            IconNameLastAyahs[id] = IconName.StickyFill;
                        }
                        return;
                    }
                }

                await localStorage.SetItemAsStringAsync(cName, cValue);
                // TODO: show toast message
                await ShowMessage("Terkahir Dibaca", message);
                
            }
            catch (Exception ex)
            {
                await ShowError($"Set ayat terakhir error: {ex.Message}");
            }
        }

        //    function setLastAyah(exdays, surahID, surahName, ayahID, id, juzID)
        //    {
        //        var expires = "expires=Thu, 01 Jan 1970 00:00:00"
        //var cname = "lastAyah";
        //        var isValid = checkCookie(cname);
        //        var oldValue = "";
        //        var isConfirmed = false;
        //        var cvalue = "";
        //        var message = "Ayat terakhir dibaca berhasil diubah menjadi Q.S. " + surahID + ". " + surahName + " Ayat " + ayahID + ".";
        //        if (!isValid)
        //        {
        //            var temp = getCookie(cname);
        //            var splittedText = temp.split("|");

        //            if (splittedText.length > 2)
        //            {
        //                splittedText[0];

        //        //isConfirmed = confirm('Ayat terakhir dibaca Q.S. ' + splittedText[0] + '. ' + splittedText[1] + ' Ayat ' + splittedText[2] + ' akan diganti menjadi Q.S.' + surahID + '. ' + surahName + ' Ayat ' + ayahID + '. Anda Yakin ?');
        //        //$.confirm({
        //        //    title: 'Konfirmasi',
        //        //    content: "Ayat terakhir dibaca Q.S. " + splittedText[0] + ". " + splittedText[1] + " Ayat " + splittedText[2] + " akan diganti menjadi Q.S." + surahID + ". " + surahName + " Ayat " + ayahID + ". Anda Yakin ?",
        //        //    buttons: {
        //        //        confirm: function () {
        //        //            cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
        //        //            const d = new Date();
        //        //            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        //        //            expires = "expires=" + d.toUTCString();
        //        //            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        //        //            showToast(message);
        //        //        },
        //        //        cancel: function () {
        //        //        }
        //        //    }
        //        //});

        //        $.confirm({
        //                title: 'Konfirmasi',
        //            content: "Ayat terakhir dibaca Q.S. " + splittedText[0] + ". " + splittedText[1] + " Ayat " + splittedText[2] + " akan diganti menjadi Q.S." + surahID + ". " + surahName + " Ayat " + ayahID + ". Anda Yakin ?",
        //            autoClose: 'cancel|10000',
        //            buttons:
        //                    {
        //                    ok:
        //                        {
        //                        text: 'OK',
        //                    action: function() {
        //                                //$('#ayahModal').modal('hide');
        //                                cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
        //                                const d = new Date();
        //                                d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        //                                expires = "expires=" + d.toUTCString();
        //                                document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        //                                showToast(message);
        //                            }
        //                        },
        //                cancel: function() {
        //                        }
        //                    }
        //                });
        //            }
        //            else
        //            {
        //                cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
        //                const d = new Date();
        //                d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        //                expires = "expires=" + d.toUTCString();
        //                document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        //                showToast(message);
        //            }
        //        }
        //        else
        //        {
        //            cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
        //            const d = new Date();
        //            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        //            expires = "expires=" + d.toUTCString();
        //            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        //            showToast(message);
        //        }
        //    }

        protected async Task ShowError(string errorMessage)
        {
            await Swal.FireAsync("Error", errorMessage, SweetAlertIcon.Error);
        }

        protected async Task ShowMessage(string title, string message)
        {

            //await Swal.FireAsync(title, message, SweetAlertIcon.Success); 
            await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Konfirmasi",
                Text = message,
                Icon = SweetAlertIcon.Success,
                ShowCancelButton = false,
                ConfirmButtonText = "Ok",
                TimerProgressBar = true,
                Timer = 3000,
            });
        }
    }
}
