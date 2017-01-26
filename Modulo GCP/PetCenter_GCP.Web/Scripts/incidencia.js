function addQueueUploadCom(id, filename, isTemp, parentRow, removeUpload) {

    var $div = document.createElement('div');
    $div.id = 'container' + id;
    $div.style.setProperty('border', '1px solid #A4A4A4');
    $div.style.setProperty('padding', '2px 5px 5px 5px');
    $div.style.setProperty('margin', '.3em 0');
    $div.setAttribute('class', 'ui-corner-all');

    var $a = document.createElement('a');
    $a.id = 'queue' + id;
    $a.setAttribute('target', '_blank');
    $a.innerHTML = shortFileName(filename);
    $div.appendChild($a);

    var $a1 = document.createElement('a');
    $a1.setAttribute('class', 'ui-icon ui-icon-circle-close');
    $a1.style.setProperty('float', 'right');
    $a1.style.setProperty('cursor', 'pointer');
    $a1.setAttribute('onclick', removeUpload + '("' + id + '");');
    $div.appendChild($a1);

    var $span1 = document.createElement('span');
    $span1.id = 'queue' + id + 'progress';
    $span1.style.setProperty('float', 'right');
    $span1.style.setProperty('padding-right', '10px');
    $span1.style.setProperty('margin-top', '1.5px');
    $span1.setAttribute('tempid', id + (isTemp || ''));
    $div.appendChild($span1);
    //$.dlg("#queuelist").append($div);
    $.dlg("#queuelist" + (parentRow || '')).append($div);
}

function addQueueUploadComMultiple(queue, id, filename, isTemp, parentRow, removeUpload) {

    var $div = document.createElement('div');
    $div.id = 'container' + queue + id;
    $div.style.setProperty('border', '1px solid #A4A4A4');
    $div.style.setProperty('padding', '2px 5px 5px 5px');
    $div.style.setProperty('margin', '.3em 0');
    $div.setAttribute('class', 'ui-corner-all');

    var $a = document.createElement('a');
    //$a.id = 'queue' + id;
    $a.id = queue + id;
    $a.setAttribute('target', '_blank');
    $a.innerHTML = shortFileName(filename);
    $div.appendChild($a);

    var $a1 = document.createElement('a');
    $a1.setAttribute('class', 'ui-icon ui-icon-circle-close');
    $a1.style.setProperty('float', 'right');
    $a1.style.setProperty('cursor', 'pointer');
    $a1.setAttribute('onclick', removeUpload + '("' + queue + id + '");');
    $div.appendChild($a1);

    var $span1 = document.createElement('span');
    //$span1.id = 'queue' + id + 'progress';
    $span1.id = queue + id + 'progress';
    $span1.style.setProperty('float', 'right');
    $span1.style.setProperty('padding-right', '10px');
    $span1.style.setProperty('margin-top', '1.5px');
    $span1.setAttribute('tempid', queue + id + (isTemp || ''));
    $div.appendChild($span1);
    //$.dlg("#queuelist").append($div);
    $.dlg("#queuelist" + (parentRow || '')).append($div);
}

function addQueueUpload(id, filename, isTemp, isReadOnly, parentRow, removeUpload) {
    var $div = document.createElement('div');
    $div.id = 'container' + id;
    $div.style.setProperty('border', '1px solid #A4A4A4');
    $div.style.setProperty('padding', '2px 5px 5px 5px');
    $div.style.setProperty('margin', '.3em 0');
    $div.setAttribute('class', 'ui-corner-all');
    var $a = document.createElement('a');
    $a.id = 'queue' + id;
    $a.setAttribute('target', '_blank');
    $a.innerHTML = shortFileName(filename);
    $div.appendChild($a);

    if (!isReadOnly) {
        var $a1 = document.createElement('a');
        $a1.setAttribute('class', 'ui-icon ui-icon-circle-close');
        $a1.style.setProperty('float', 'right');
        $a1.style.setProperty('cursor', 'pointer');
        $a1.setAttribute('onclick', removeUpload + '("' + id + '");');
        $div.appendChild($a1);
    }

    var $span1 = document.createElement('span');
    $span1.id = 'queue' + id + 'progress';
    $span1.style.setProperty('float', 'right');
    $span1.style.setProperty('padding-right', '10px');
    $span1.style.setProperty('margin-top', '1.5px');
    $span1.setAttribute('tempid', isTemp ? id : '');
    $div.appendChild($span1);
    $.dlg("#queuelist" + (parentRow || '')).append($div);
}

function queueComplete(index, filename, filebyte, flag) {

    elem = $.dlg("#queue" + index)[0];
    elem.setAttribute('href', '#');
    var userFileName = elem.textContent || elem.innerText;
    elem.setAttribute('onclick', 'goToFile(\"' + filename + '\",\"' + userFileName + '\",\"' + flag + '\"); return false;');

    $span = $.dlg("#queue" + index + "progress")[0];
    $span.setAttribute('flag', flag);
    $span.setAttribute('ofile', filename);
    $span.setAttribute('fbyte', filebyte);
    $span.innerHTML = "Completado";
}

function queueCompleteMultiple(queue, index, filename, filebyte, flag) {

    elem = $.dlg("#" + queue + index)[0];
    elem.setAttribute('href', '#');
    var userFileName = elem.textContent || elem.innerText;
    elem.setAttribute('onclick', 'goToFile(\"' + filename + '\",\"' + userFileName + '\",\"' + flag + '\"); return false;');

    $span = $.dlg("#" + queue + index + "progress")[0];
    $span.setAttribute('flag', flag);
    $span.setAttribute('ofile', filename);
    $span.setAttribute('fbyte', filebyte);
    $span.innerHTML = "Completado";
}

function shortFileName(filename) {
    if (filename.length > 40) {
        filename = filename.slice(0, 33) + '...' + filename.slice(-13);
    }
    return filename;
}

function goToFile(fileName, userFileName, flagTempPath) {
    var $form = document.createElement('form');
    $form.setAttribute('id', 'data_form');
    $form.setAttribute('action', baseUrl + 'Base/Visor');
    $form.setAttribute('method', 'post');
    $form.setAttribute('target', '_blank');
    document.body.appendChild($form);
    AddParameter($form, "_file", fileName);
    AddParameter($form, "_shown", userFileName);
    AddParameter($form, "_path", flagTempPath);
    $form.submit();
    document.body.removeChild($form);
}

function AddParameter(form, name, value) {
    var $input = document.createElement('input');
    $input.setAttribute('type', 'hidden');
    $input.setAttribute('name', name);
    $input.setAttribute('value', value);
    form.appendChild($input);
}