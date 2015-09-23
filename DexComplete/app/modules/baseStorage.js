function BaseStorage(parent) {
    parent.loadedData = "";
    parent.data = [];

    parent.DecryptCode = function (val) {
        var arrOut2 = val;
        var load = new Uint8Array(atob(arrOut2).split("").map(function (c) {
            return c.charCodeAt(0);
        }));
        for (var u in load) {
            var tt = 0;
            while (tt < 8 / parent.saveBits) {
                var power = Math.pow(2, (tt + 1) * parent.saveBits) - 1;
                var res = (power & load[u]) >>> (tt * parent.saveBits);
                parent.data.push(res);
                tt += 1;
            }
        }
    }


    parent.getCode = function () {
        var codeLength = Math.ceil(parent.data.length / (8 / parent.saveBits));
        var encode = new Uint8Array(codeLength + 1);
        for (var u in encode) {
            encode[u] = 0;
        }
        var bitoffset = 0;
        var byteoffset = 0;
        for (var u in parent.data) {
            var value = parseInt(parent.data[u]);
            var t = value * Math.pow(2, bitoffset);
            encode[byteoffset] += t;

            bitoffset += parent.saveBits;
            if (bitoffset >= 8) {
                bitoffset = 0;
                byteoffset += 1;
            }
        }
        var arrOut = btoa(String.fromCharCode.apply(null, encode));
        return arrOut;
    }

    parent.getValue = function (slot) {
        return parent.data[slot];
    }

    parent.setValue = function (slot, newValue) {
        if (newValue > parent.maxValue) {
            newValue = 0;
        }
        parent.data[slot] = newValue;
    }

}