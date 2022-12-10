$(function () {

    $('#Precio').bind('paste', function () {
        var self = this;
        setTimeout(function () {
            if (!/^\d*(,\d{1,3})+$/.test($(self).val())) $(self).val('');
        }, 0);
    });

    $('#Precio').keypress(function (e) {
        var character = String.fromCharCode(e.keyCode)
        var newValue = this.value + character;
        if ((newValue.length == 1 && character == ',') || (isNaN(character) && character != ',') || hasDecimalPlace(newValue, 4)) {
            e.preventDefault();
            return false;
        }
    });

    $('#Costo').bind('paste', function () {
        var self = this;
        setTimeout(function () {
            if (!/^\d*(,\d{1,3})+$/.test($(self).val())) $(self).val('');
        }, 0);
    });

    $('#Costo').keypress(function (e) {
        var character = String.fromCharCode(e.keyCode)
        var newValue = this.value + character;
        if ((newValue.length == 1 && character == ',') || (isNaN(character) && character != ',') || hasDecimalPlace(newValue, 4)) {
            e.preventDefault();
            return false;
        }
    });

    function hasDecimalPlace(value, x) {
        var pointIndex = value.indexOf(',');
        return pointIndex >= 0 && pointIndex < value.length - x;
    }
});