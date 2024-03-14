/*const { Toast } = require("bootstrap");*/

function scrollToAyah(ayahID) {
    if (ayahID == '' || ayahID == '1') {
        window.scrollTo(0, 0);
        return;
    }

    $('html, body').animate({
        scrollTop: $("#div_" + ayahID).offset().top - 100
    }, 500);
}

function scrollToJuz(juzID) {
    if (juzID == '' || juzID == '1') {
        window.scrollTo(0, 0);
        return;
    }

    $('html, body').animate({
        scrollTop: $("#div_" + juzID).offset().top - 100
    }, 500);
}

function copyToClipboard(index, ayahID, surahID, ayah, textIndo, translateIndo) {
    var copiedText = "Q.S. " + surahID + ":" + (parseInt(index) + 1) + "\n\n" + ayah + "\n\n" + textIndo + "\n\n" + translateIndo + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;

    var $temp = $("<textarea>");

    $("body").append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();
    //var txtCopy = $("#txtCopy");
    //txtCopy.val(copiedText).select();
    //document.execCommand("copy");

    $("#btnCopy" + index).focus();
    //alert("Q.S. " + surahID + ":" + ayahID + " berhasil disalin.")
    //$.alert({
    //    title: 'Salin Ayat',
    //    content: "Q.S. " + surahID + ":" + ayahID + " berhasil disalin.",
    //});
    $.confirm({
        title: 'Salin Ayat',
        content: "Q.S. " + surahID + ":" + ayahID + " berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    //$('#ayahModal').modal('hide');
                }
            }
        }
    });
}

function copyHadith(index, name, hadithNo, arabText, translateIndo) {
    var txt = document.createElement("textarea");
    txt.innerHTML = translateIndo;

    var copiedText = "H.R. " + name + " No. " + hadithNo + "\n\n" + arabText + "\n\n" + txt.value + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;

    var $temp = $("<textarea>");

    $("body").append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();

    $("#btnCopy" + index).focus();

    $.confirm({
        title: 'Salin Hadis',
        content: "H.R. " + name + " No. " + hadithNo + " berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    //$('#ayahModal').modal('hide');
                }
            }
        }
    });
}

function copyHadithArbain(index, judul, hadithNo, arabText, translateIndo) {
    var txt = document.createElement("textarea");
    txt.innerHTML = translateIndo;

    var copiedText = "Hadis Arbain No. " + hadithNo + " " + judul + "\n\n" + arabText + "\n\n" + txt.value + "\n\n*Via MyQuranWeb Indonesia \n"  + window.location.origin;

    var $temp = $("<textarea>");

    $("body").append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();

    $("#btnCopy" + index).focus();

    $.confirm({
        title: 'Salin Hadis',
        content: "Hadis Arbain No. " + hadithNo + " berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    //$('#ayahModal').modal('hide');
                }
            }
        }
    });
}

function copyHadithBM(index, hadithNo, arabText, translateId) {
    var txt = document.createElement("textarea");
    txt.innerHTML = translateId;

    var copiedText = "Hadis Bulughul Maram No. " + hadithNo + "\n\n" + arabText + "\n\n" + txt.value + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;

    var $temp = $("<textarea>");

    $("body").append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();

    $("#btnCopy" + index).focus();

    $.confirm({
        title: 'Salin Hadis',
        content: "Hadis Bulughul Maram No. " + hadithNo + " berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    //$('#ayahModal').modal('hide');
                }
            }
        }
    });
}
function copyAsmaulHusna(index, judul, no, arabText, translateIndo) {
    var txt = document.createElement("textarea");
    txt.innerHTML = translateIndo;

    var copiedText = "Asmaul Husna No. " + no + " " + judul + "\n\n" + arabText + "\n\n" + txt.value + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;

    var $temp = $("<textarea>");

    $("body").append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();

    $("#btnCopy" + index).focus();

    $.confirm({
        title: 'Salin Asmaul Husna',
        content: "Asmaul Husna No. " + no + " berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    //$('#ayahModal').modal('hide');
                }
            }
        }
    });
}

//function copyAyah(ayahID, surahID, ayah, textIndo, translateIndo) {
//    var copiedText = "Q.S. " + surahID + ":" + ayahID + "\n" + ayah + "\n" + textIndo + "\n" + translateIndo;

//    var $temp = $("<textarea id='txtCopiedAyah'>");

//    document.body.append($temp);
//    $temp.val(copiedText).select();
//    document.execCommand("copy");
//    $temp.remove();

//    //$.alert({
//    //    title: 'Salin Ayat',
//    //    content: "Q.S. " + surahID + ":" + ayahID + " berhasil disalin.",
//    //});
//    $.confirm({
//        title: 'Salin Ayat',
//        content: "Q.S. " + surahID + ":" + ayahID + " berhasil disalin.",
//        autoClose: 'ok|2000',
//        buttons: {
//            ok: {
//                text: 'OK',
//                action: function () {
//                    $('#ayahModal').modal('hide');
//                }
//            }
//        }
//    });
//}

function copyAyah() {
    var modal = $("#ayahModal");
    var title = modal.find('.modal-title').text() + "\n\n";
    var readText = modal.find('.modal-body #ayahReadText').html() + "\n";
    var readTextIndo = modal.find('.modal-body #ayahReadIndo').html() + "\n";
    var translateIndo = modal.find('.modal-body #ayahTranslateIndo').html() + "\n";

    var copiedText = title + readText + readTextIndo + translateIndo + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;

    var $temp = $("<textarea id='txtCopiedAyah'>");

    modal.append($temp);
    $temp.val(copiedText).select();    
    document.execCommand("copy");
    $temp.remove();

    $.confirm({
        title: 'Salin Ayat',
        content: title + " berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    $('#ayahModal').modal('hide');
                }
            }
        }
    });
}

function copyTafsirText(index, surahID, tafsirText) {
    var copiedText = "Tafsir Kemenag Q.S. " + surahID + ":" + (parseInt(index) + 1) + "\n" + tafsirText + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;
    var $temp = $("<textarea>");

    $("body").append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();

    $("#btnCopy" + index).focus();
    //alert("Tafsir Kemenag Q.S. " + surahID + ":" + (parseInt(index) + 1) + " berhasil disalin.")
    //$.alert({
    //    title: 'Salin Tafsir',
    //    content: "Tafsir Kemenag Q.S. " + surahID + ":" + (parseInt(index) + 1) + " berhasil disalin.",
    //});

    $.confirm({
        title: 'Salin Tafsir',
        content: "Tafsir Kemenag Q.S. " + surahID + ":" + (parseInt(index) + 1) + " berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    $('#tafsirModal').modal('hide');
                }
            }
        }
    });
}

function copyTafsir() {
    var modal = $("#tafsirModal");
    var copiedText = modal.find('.modal-title').text() + "\n\n" + modal.find('.modal-body #tafsirText').html() + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;
    var $temp = $("<textarea>");

    modal.append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();
    //alert("Tafsir berhasil disalin.");
    //$.alert({
    //    title: 'Salin Tafsir',
    //    content: "Tafsir berhasil disalin.",
    //});
    $.confirm({
        title: 'Salin Tafsir',
        content: "Tafsir berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    $('#tafsirModal').modal('hide');
                }
            }
        }
    });
}

function copyTafsirBasedOnIndex(index) {
    //var modal = $("#tafsirModal");
    var btnCopy = $("#btnCopy" + index);
    //$(this).data("id")
    var copiedText = btnCopy.data("title") + "\n" + btnCopy.data("tafsir") + "\n\n*Via MyQuranWeb Indonesia \n" + window.location.origin;  //modal.find('.modal-title').text() + "\n\n" + modal.find('.modal-body #tafsirText').html();

    var $temp = $("<textarea>");
    $("body").append($temp);
    $temp.val(copiedText).select();
    document.execCommand("copy");
    $temp.remove();

    $.confirm({
        title: 'Salin Tafsir',
        content: "Tafsir berhasil disalin.",
        autoClose: 'ok|2000',
        buttons: {
            ok: {
                text: 'OK'
            }
        }
    });
}

function setLastAyah(exdays, surahID, surahName, ayahID, id, juzID) {
    var expires = "expires=Thu, 01 Jan 1970 00:00:00"
    var cname = "lastAyah";
    var isValid = checkCookie(cname);
    var oldValue = "";
    var isConfirmed = false;
    var cvalue = "";
    var message = "Ayat terakhir dibaca berhasil diubah menjadi Q.S. " + surahID + ". " + surahName + " Ayat " + ayahID + ".";
    if (!isValid) {
        var temp = getCookie(cname);
        var splittedText = temp.split("|");

        if (splittedText.length > 2) {
            splittedText[0];

            //isConfirmed = confirm('Ayat terakhir dibaca Q.S. ' + splittedText[0] + '. ' + splittedText[1] + ' Ayat ' + splittedText[2] + ' akan diganti menjadi Q.S.' + surahID + '. ' + surahName + ' Ayat ' + ayahID + '. Anda Yakin ?');
            //$.confirm({
            //    title: 'Konfirmasi',
            //    content: "Ayat terakhir dibaca Q.S. " + splittedText[0] + ". " + splittedText[1] + " Ayat " + splittedText[2] + " akan diganti menjadi Q.S." + surahID + ". " + surahName + " Ayat " + ayahID + ". Anda Yakin ?",
            //    buttons: {
            //        confirm: function () {
            //            cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
            //            const d = new Date();
            //            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            //            expires = "expires=" + d.toUTCString();
            //            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
            //            showToast(message);
            //        },
            //        cancel: function () {
            //        }
            //    }
            //});

            $.confirm({
                title: 'Konfirmasi',
                content: "Ayat terakhir dibaca Q.S. " + splittedText[0] + ". " + splittedText[1] + " Ayat " + splittedText[2] + " akan diganti menjadi Q.S." + surahID + ". " + surahName + " Ayat " + ayahID + ". Anda Yakin ?",
                autoClose: 'cancel|10000',
                buttons: {
                    ok: {
                        text: 'OK',
                        action: function () {
                            //$('#ayahModal').modal('hide');
                            cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
                            const d = new Date();
                            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
                            expires = "expires=" + d.toUTCString();
                            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
                            showToast(message);
                        }
                    },
                    cancel: function () {
                    }
                }
            });
        }
        else {
            cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
            const d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
            showToast(message);
        }
    }
    else {
        cvalue = surahID + "|" + surahName + "|" + ayahID + "|" + id + "|" + juzID;
        const d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        showToast(message);
    }
}

function setCookie(cname, cvalue, exdays) {
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
}

function settingApp(cname, cvalue, exdays, title) {
    var isValid = checkCookie(cname);
    var expires = "expires=Thu, 01 Jan 1970 00:00:00"
    if (!isValid) {
        //if (cname == "ukuranTeksSetting" || cname == "qariSetting") {
            const d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            expires = "expires=" + d.toUTCString();

            // Set to zero (0)
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        //}
        //else {
        //    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        //}

        $.confirm({
            title: 'Setting',
            content: title + " berhasil diubah.",
            autoClose: 'ok|1000',
            buttons: {
                ok: {
                    text: 'OK',
                    action: function () {
                    }
                }
            }
        });
    }
    else {
        const d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        expires = "expires=" + d.toUTCString();
        document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";

        $.confirm({
            title: 'Setting',
            content: title + " berhasil diubah.",
            autoClose: 'ok|1000',
            buttons: {
                ok: {
                    text: 'OK',
                    action: function () {
                    }
                }
            }
        });
    }
}

function deleteBookmark(cname, description, url) {
    //var isConfirmed = confirm('Apakah anda yakin ingin menghapus bookmark ?')
    //$.confirm({
    //    title: 'Konfirmasi',
    //    content: "Apakah anda yakin ingin menghapus bookmark ?",
    //    buttons: {
    //        confirm: function () {
    //            var isValid = checkCookie(cname);
    //            var expires = "expires=Thu, 01 Jan 1970 00:00:00"

    //            if (!isValid) {
    //                document.cookie = cname + "=" + cname + ";" + expires + ";path=/";
    //            }

    //            if (url != "") {
    //                var origin = window.location.origin;
    //                window.location.replace(origin + url);
    //            }
    //        },
    //        cancel: function () {
    //        }
    //    }
    //});

    $.confirm({
        title: 'Konfirmasi',
        content: "Apakah anda yakin ingin menghapus " + description + " dari daftar bookmark ?",        
        autoClose: 'cancel|8000',
        buttons: {
            ok: {
                text: 'OK',
                action: function () {
                    var isValid = checkCookie(cname);
                    var expires = "expires=Thu, 01 Jan 1970 00:00:00"

                    if (!isValid) {
                        document.cookie = cname + "=" + cname + ";" + expires + ";path=/";
                        //Toast(description + " berhasil dihapus dari daftar bookmark.");
                    }
                    if (url != "") {
                        var origin = window.location.origin;
                        window.location.replace(origin + url);
                    }

                    //var millisecondsToWait = 1000;
                    //setTimeout(function () {
                    //    if (url != "") {
                    //        var origin = window.location.origin;
                    //        window.location.replace(origin + url);
                    //    }
                        
                    //}, millisecondsToWait);
                }
            },
            cancel: function () {
            }
        }
    });
}

function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }

    return "";
}

function checkCookie(id) {
    var cookie = getCookie(id);
    //if (id != "") {
    //    alert("Welcome again " + user);
    //} else {
    //    user = prompt("Please enter your name:", "");
    //    if (id != "" && id != null) {
    //        setCookie("id", id, 365);
    //    }
    //}
    if (cookie != "") {
        //alert(cookie);
        return false;
    }
    else {
        //setCookie(id, id, 365);
        return true;
    }
}

function playAudio(id) {
    var audio = document.getElementById('player' + id);
    var playBtn = $("#playBtn" + id)
    if (audio.currentTime > 0 && !audio.paused) {
        audio.pause();
    } else {
        playBtn.removeClass();
        playBtn.addClass("fa fa-pause");
        audio.play();
    }
}

function pauseAudio(id) {
    var playBtn = $("#playBtn" + id)
    playBtn.removeClass();
    playBtn.addClass("fa fa-play");
}

function playAudioSurah(controlName) {
    var audio = document.getElementById("audioElement");
    var playBtn = $("#" + controlName)
    if (audio.currentTime > 0 && !audio.paused) {
        playBtn.removeClass();
        playBtn.addClass("fa fa-play");
        audio.pause();
    } else {
        playBtn.removeClass();
        playBtn.addClass("fa fa-pause");
        audio.play();
    }
}

function muteAudio(id) {
    var playerID = document.getElementById('player' + id);

    playerID.muted = !playerID.muted
}

function openAyah(surahID, ayahID, baseUrl) {
    var origin = window.location.origin;
    if (!(baseUrl === undefined)) {
        origin = baseUrl;
    }
    //window.location.href = origin + "/Quran/SurahDetail/" + surahID + '/' + ayahID;    
    window.open(origin + "/Quran/SurahDetail/" + surahID + '/' + ayahID, "_blank");
}

function openJuz(juzID, ayahID, baseUrl) {
    var origin = window.location.origin;
    if (!(baseUrl === undefined)) {
        origin = baseUrl;
    }
    //window.location.href = origin + "/Quran/SurahDetail/" + surahID + '/' + ayahID;    
    window.open(origin + "/Quran/JuzDetail/" + juzID + '/' + ayahID, "_blank");
}

function showToast(message) {
    var x = document.getElementById("snackbar");
    x.innerHTML = message;
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}