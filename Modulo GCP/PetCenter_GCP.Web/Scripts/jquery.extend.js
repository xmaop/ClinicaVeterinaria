$.dlg = function (selector) {
    return $(dlgrFocus).find(selector);
}
jQuery.fn.extend({
    valdlg: function (value) {
        if (value == undefined)
            return $.trim($(dlgrFocus).find(this.selector).val());
        else
            return $.trim($(dlgrFocus).find(this.selector).val(value));
    },
    textdlg: function (value) {
        if (value == undefined)
            return $.trim($(dlgrFocus).find(this.selector).text());
        else
            return $.trim($(dlgrFocus).find(this.selector).text(value));
    },
    selectRange: function (start, end) {
        if (!end) end = start;
        return this.each(function () {
            if (this.setSelectionRange) {
                this.focus();
                this.setSelectionRange(start, end);
            } else if (this.createTextRange) {
                var range = this.createTextRange();
                range.collapse(true);
                range.moveEnd('character', end);
                range.moveStart('character', start);
                range.select();
            }
        });
    },
    appendOnfocus: function (value) {
        value = $.trim(value);
        return this.each(function () {
            if ($(this).length != 0) {
                var charPos = $(this).get(0).selectionStart;
                $(this).val($(this).val().substring(0, charPos) + value + $(this).val().substring(charPos));
                $(this).selectRange(charPos + value.length);
            }
        });
    }
});

$.cbx = function (selector) {
    return $("#colorbox").find(selector);
}
